using ExpenseTracker.Contracts.Logging.Generic;
using ExpenseTracker.Contracts.Services;
using ExpenseTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using static ExpenseTracker.Controllers.RouteNames.AuthenticationRoutes;

namespace ExpenseTracker.Controllers.Controllers;

[ApiController]
[Route(Base)]
public class AuthenticationController(
    IServiceManager serviceManager, 
    ILoggerManager<AuthenticationController> logger
) : ControllerBase
{
    [HttpPost(Authenticate)]
    public async Task<IActionResult> Login([FromBody] LoginInfo loginInfo)
    {
		try
		{
            logger.LogDebug("Login attempt for user {Username}", loginInfo.Username);

            var response = await serviceManager.AuthenticationService.Authenticate(loginInfo).ConfigureAwait(false);

            logger.LogInfo("User {Username} authenticated successfully", loginInfo.Username);

            return Ok(response);
        }
		catch (Exception ex)
		{
            logger.LogError(ex, "Authentication failed for user {Username}", loginInfo.Username);
            return Unauthorized();
		}
    }
}
