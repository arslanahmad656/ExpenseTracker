using ExpenseTracker.Contracts.Logging.Generic;
using ExpenseTracker.Contracts.Services;
using ExpenseTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using static ExpenseTracker.Controllers.RouteNames;

namespace ExpenseTracker.Controllers.Controllers;

[ApiController]
[Route(AuthenticationControllerBase)]
public class AuthenticationController(IServiceManager serviceManager, ILoggerManager<AuthenticationController> logger) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginInfo loginInfo)
    {
		try
		{
            logger.LogDebug("Login attempt for user {Username}", loginInfo.Username);

            var token = await serviceManager.AuthenticationService.Authenticate(loginInfo).ConfigureAwait(false);

            logger.LogInfo("User {Username} authenticated successfully", loginInfo.Username);

            return Ok(token);
        }
		catch (Exception ex)
		{
            logger.LogError(ex, "Authentication failed for user {Username}", loginInfo.Username);
            return Unauthorized();
		}
    }
}
