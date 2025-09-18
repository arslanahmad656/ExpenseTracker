using ExpenseTracker.Shared.Models;

namespace ExpenseTracker.Contracts.Services;

public interface IAuthenticationService
{
    Task<AuthenticationResponse> Authenticate(LoginInfo userLoginInfo);

    string? GetCurrentUserClaimValue(string claimType);

    string? GetCurrentUserId();

    bool IsCurrentUserInRole(string role);
}
