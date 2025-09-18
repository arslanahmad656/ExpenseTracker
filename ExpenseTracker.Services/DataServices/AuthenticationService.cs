using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ExpenseTracker.Contracts;
using ExpenseTracker.Contracts.Logging.Generic;
using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Contracts.Services;
using ExpenseTracker.Shared.Models;
using ExpenseTracker.Shared.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ExpenseTracker.Services.DataServices;

public partial class AuthenticationService(
    IRepositoryManager repositoryManager, 
    ILoggerManager<AuthenticationService> logger, 
    IPasswordHasher passwordHasher,
    IOptions<JwtOptions> jwtOptions,
    IHttpContextAccessor httpContext
) : IAuthenticationService
{
    private readonly JwtOptions jwtOptions = jwtOptions.Value;
    private readonly HttpContext httpContext = httpContext.HttpContext;

    public async Task<AuthenticationResponse> Authenticate(LoginInfo userLoginInfo)
    {
        logger.LogInfo("Authenticating user {Username}", userLoginInfo.Username);

        logger.LogDebug($"Fetching user {userLoginInfo.Username} from database.");

        var user = await repositoryManager.PrincipalRepository.GetActiveUser(userLoginInfo.Username).ConfigureAwait(false);
        logger.LogDebug("User {Username} fetched from database.", userLoginInfo.Username);
        logger.LogDebug("Validating the user credentials.");

        if(!passwordHasher.Verify(userLoginInfo.Password, user.PasswordHash))
        {
            logger.LogWarn("Invalid password for user {Username}", userLoginInfo.Username);
            throw new UnauthorizedAccessException("Invalid credentials.");
        }

        logger.LogDebug("User credentials validated.");
        logger.LogDebug("Generating the token for user {Username}", userLoginInfo.Username);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, user.Username)
        };
        claims.AddRange(user.UserRoles.Select(r => new Claim(ClaimTypes.Role, r.Role.Name)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(jwtOptions.ExpiresMinutes);

        logger.LogDebug("Data ready for token generation. Generating the token now.");

        var token = new JwtSecurityToken(
            issuer: jwtOptions.Issuer,
            audience: jwtOptions.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return new(tokenString, expires, new(user.Username, user.UserRoles.MinBy(ur => ur.Role.Priority)?.Role?.Name ?? string.Empty, [.. user.UserRoles.Select(ur => ur.Role.Name)]));
    }

    public string? GetCurrentUserClaimValue(string claimType)
    {
        var user = httpContext?.User;
        if (user?.Claims is null)
        {
            return null;
        }

        var value = user.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;

        return value;
    }

    public string? GetCurrentUserId() => GetCurrentUserClaimValue(ClaimTypes.NameIdentifier);

    public bool IsCurrentUserInRole(string role)
    {
        var user = httpContext?.User;
        if (user is null)
        {
            return false;
        }

        return user.IsInRole(role);
    }
}
