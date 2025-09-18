using ExpenseTracker.Contracts.Logging.Generic;
using ExpenseTracker.Contracts.Services;
using ExpenseTracker.Controllers.RouteNames;
using ExpenseTracker.Entities.Models;
using ExpenseTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers.Controllers;

[ApiController]
[Route(FormRoutes.Base)]
public class FormController(
	IServiceManager serviceManager,
	ILoggerManager<FormController> logger
) : ControllerBase
{
    [HttpPost(FormRoutes.CreateNew)]
    public async Task<IActionResult> Create([FromBody] CreateExpenseForm model)
    {
		try
		{
			logger.LogInfo($"Creating new expense form.");

			logger.LogDebug("Invoking the form service.");

			var result = await serviceManager.FormService.SubmitExpenseForm(model.Form, model.Expenses).ConfigureAwait(false);

			logger.LogInfo("Created expense form with id {id}", result);

			return CreatedAtAction(nameof(GetForm), new { formId = result }, result);
		}
		catch (Exception ex)
		{
			var msg = "Error occurred while creating a new expense.";

            logger.LogError(ex, msg);

			return StatusCode(500, msg);
		}
    }

	[HttpGet(FormRoutes.GetFormComplete)]
	public async Task<IActionResult> GetForm(int formId)
	{
		logger.LogInfo("Fetching the form with id {formId}", formId);

		try
		{
			logger.LogDebug($"Initiating service call.");
			var form = await serviceManager.FormService.GetFormDetailedOwnedByCurrentUser(formId).ConfigureAwait(false);
			logger.LogDebug("Service call to get form {formId} successful.", formId);

			return Ok(form);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Error occurred while getting the form for the current user id.");

			return StatusCode(500, ex.Message);
		}
	}

	[HttpPost(FormRoutes.CancelExpense)]
	public async Task<IActionResult> CancelExpense(int expenseId, [FromBody] DenialModel model)
	{
		await serviceManager.FormService.CancelExpense(expenseId, model.Reason).ConfigureAwait(false);

		return Ok();
	}

    [HttpPost(FormRoutes.CancelForm)]
    public async Task<IActionResult> CancelForm(int formId, [FromBody] DenialModel model)
    {
        await serviceManager.FormService.CancelForm(formId, model.Reason).ConfigureAwait(false);

        return Ok();
    }

    [HttpPost(FormRoutes.RejectExpense)]
    public async Task<IActionResult> RejectExpense(int expenseId, [FromBody] DenialModel model)
    {
        await serviceManager.FormService.RejectExpense(expenseId, model.Reason).ConfigureAwait(false);

        return Ok();
    }

    [HttpPost(FormRoutes.RejectForm)]
    public async Task<IActionResult> RejectForm(int formId, [FromBody] DenialModel model)
    {
        await serviceManager.FormService.RejectForm(formId, model.Reason).ConfigureAwait(false);

        return Ok();
    }

    [HttpPost(FormRoutes.ApproveExpense)]
    public async Task<IActionResult> ApproveExpense(int expenseId)
    {
        await serviceManager.FormService.ApproveExpense(expenseId).ConfigureAwait(false);

        return Ok();
    }

    [HttpPost(FormRoutes.ApproveForm)]
    public async Task<IActionResult> ApproveForm(int formId)
    {
        await serviceManager.FormService.ApproveForm(formId).ConfigureAwait(false);

        return Ok();
    }

    [HttpPost(FormRoutes.ReimburseExpense)]
    public async Task<IActionResult> ReimburseExpense(int expenseId)
    {
        await serviceManager.FormService.ReimburseExpense(expenseId).ConfigureAwait(false);

        return Ok();
    }

    [HttpPost(FormRoutes.ReimburseForm)]
    public async Task<IActionResult> ReimburseForm(int formId)
    {
        await serviceManager.FormService.ReimburseForm(formId).ConfigureAwait(false);

        return Ok();
    }

    [HttpPut(FormRoutes.UpdateForm)]
    public async Task<IActionResult> UpdateForm(int formId, [FromBody] UpdateFormComposite form)
    {
        await serviceManager.FormService.UpdateForm(form.Form with { Id = formId }, form.Expenses).ConfigureAwait(false);

        return Ok();
    }
}
