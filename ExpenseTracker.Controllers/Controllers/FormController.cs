using ExpenseTracker.Contracts.Logging.Generic;
using ExpenseTracker.Contracts.Services;
using ExpenseTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using static ExpenseTracker.Controllers.RouteNames.FormRoutes;

namespace ExpenseTracker.Controllers.Controllers;

[ApiController]
[Route(Base)]
public class FormController(
	IServiceManager serviceManager,
	ILoggerManager<FormController> logger
) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateExpenseFormModel model)
    {
		try
		{
			logger.LogInfo($"Creating new expense form.");

			logger.LogDebug("Invoking the form service.");

			var result = await serviceManager.FormService.SubmitExpenseForm(model).ConfigureAwait(false);

			logger.LogInfo("Created expense form with id {id}", result);

			return Ok(result);	// TODO: Should be Created or CreatedAt
		}
		catch (Exception ex)
		{
			var msg = "Error occurred while creating a new expense.";

            logger.LogError(ex, msg);

			return StatusCode(500, msg);
		}
    }
}
