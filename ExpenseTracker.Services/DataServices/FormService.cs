using System.Runtime.InteropServices;
using AutoMapper;
using ExpenseTracker.Contracts;
using ExpenseTracker.Contracts.Logging.Generic;
using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Contracts.Services;
using ExpenseTracker.Entities.Models;
using ExpenseTracker.Shared.DataTransferObjects;
using ExpenseTracker.Shared.Enums;
using ExpenseTracker.Shared.Models;

namespace ExpenseTracker.Services.DataServices;

public partial class FormService(
    IRepositoryManager repositoryManager,
    ILoggerManager<FormService> logger,
    ITrackingIdGenerator trackingIdGenerator,
    IAuthenticationService authenticationService,
    IFormHistoryService formHistoryService,
    IMapper mapper,
    ISerializer serializer
) : IFormService
{
    private readonly IRepositoryManager repositoryManager = repositoryManager;

    public async Task<int> SubmitExpenseForm(CreateExpenseFormModel expenseForm, IEnumerable<CreateExpenseModel> expenses)
    {
        logger.LogInfo("Creating expense form {@expenseForm}.", expenseForm);
        logger.LogDebug("Validating the arguments.");

        await ValidateForCreation(expenseForm, expenses).ConfigureAwait(false);
        logger.LogDebug($"Validation successful.");

        logger.LogDebug("Getting current context user's ID.");

        var userId = authenticationService.GetCurrentUserId();
        if (userId is null || !int.TryParse(userId, out var userIdParsed))
        {
            logger.LogWarn($"Could not retrieve the current context user id.");
            throw new ApplicationException($"Could not retrieve the current context user id. Cannot continue futher.");
        }

        logger.LogInfo($"Context user id: {userIdParsed}.");

        logger.LogDebug($"Getting the currency by code {expenseForm.CurrencyCode}.");
        var currency = await repositoryManager.CurrencyRepository.GetCurrencyByCode(expenseForm.CurrencyCode).ConfigureAwait(false);
        if (currency is null)
        {
            logger.LogWarn($"Currency not found with code {expenseForm.CurrencyCode}.");
            throw new InvalidOperationException($"Cannot create an expense form without a valid currency.");
        }

        var now = DateTime.Now;
        logger.LogDebug("Preparing the entity for insertion.");
        var form = PrepareFormForCreation(expenseForm.Title, currency.Id, now, expenses);

        logger.LogDebug("Performing insertion.");
        await repositoryManager.FormRepository.Create(form).ConfigureAwait(false);
        await repositoryManager.Save();
        logger.LogInfo($"Expense form created with id {form.Id}.");

        logger.LogDebug("Saving histories.");
        await formHistoryService.LogFormSubmitted(form.Id, now, userIdParsed).ConfigureAwait(false);
        logger.LogDebug("Histories saved.");

        return form.Id;
    }

    public async Task<FormDto> GetFormDetailedOwnedByCurrentUser(int formId)
    {
        logger.LogDebug("Getting expense form with id {formId}", formId);

        var form = await GetFormOwnedByCurrentUser(formId).ConfigureAwait(false);

        logger.LogDebug("Retrieved form with id {formId}", formId);

        var formDto = mapper.Map<FormDto>(form);
        formDto = formDto with
        {
            LastUpdatedOn = form.FormHistories.MaxBy(fh => fh.RecordedDate)?.RecordedDate ?? throw new ApplicationException($"Could not determine the last update date f the form {formDto.Id}."),
            Expenses = [.. form.Expenses.Select(e => mapper.Map<Expense, ExpenseDto>(e) with
            {
                LastUpdatedOn = e.ExpenseHistories.MaxBy(eh => eh.RecordedDate)?.RecordedDate ?? throw new ApplicationException($"Could not determine the last update date of the expense {e.Id}.")
            })]
        };

        return formDto;
    }

    public async Task CancelExpense(int expenseId, string reason)
    {
        logger.LogInfo("Cancelling expense with id {expenseId}", expenseId);
        var expense = await ValidateExpenseCancellation(expenseId).ConfigureAwait(false);

        logger.LogDebug("Updating expense state.");
        expense.Status = ExpenseStatus.Cancelled;
        repositoryManager.ExpenseRepository.Update(expense);

        await repositoryManager.Save().ConfigureAwait(false);

        logger.LogDebug("Expense updated. Logging histories.");

        await formHistoryService.LogExpenseCancelled(expenseId, DateTime.Now, GetCurrentUserId(), reason).ConfigureAwait(false);

        logger.LogDebug("Histories logged.");
    }

    public async Task CancelForm(int formId, string reason)
    {
        logger.LogInfo("Cancelling form with id {formId} with reason {reason}", formId, reason);

        var form = await ValidateFormCancellation(formId).ConfigureAwait(false);

        logger.LogDebug("Updating form status.");
        form.Status = FormStatus.Cancelled;
        repositoryManager.FormRepository.Update(form);

        await repositoryManager.Save().ConfigureAwait(false);

        logger.LogDebug("Form status updated. Logging histories.");
        await formHistoryService.LogFormCancelled(formId, DateTime.Now, GetCurrentUserId(), reason).ConfigureAwait(false);
        logger.LogDebug("Histories updated.");
    }

    public async Task RejectExpense(int expenseId, string reason)
    {
        logger.LogInfo("Rejecting expense {id} with {reason}", expenseId, reason);

        var expense = await ValidateExpenseManagerialAction(expenseId).ConfigureAwait(false);
        logger.LogDebug("Updating the expense status.");

        expense.Status = ExpenseStatus.Rejected;
        repositoryManager.ExpenseRepository.Update(expense);

        await repositoryManager.Save().ConfigureAwait(false);

        await formHistoryService.LogExpenseRejected(expenseId, DateTime.Now, GetCurrentUserId(), reason);
    }

    public async Task ApproveExpense(int expenseId)
    {
        logger.LogInfo("Approving expense {i}", expenseId);

        var expense = await ValidateExpenseManagerialAction(expenseId).ConfigureAwait(false);
        logger.LogDebug("Updating the expense status.");

        expense.Status = ExpenseStatus.PendingReimbursement;
        repositoryManager.ExpenseRepository.Update(expense);

        await repositoryManager.Save().ConfigureAwait(false);

        await formHistoryService.LogExpenseApproved(expenseId, DateTime.Now, GetCurrentUserId());
    }

    public async Task RejectForm(int formId, string reason)
    {
        logger.LogInfo("Rejecting form {id} with {reason}", formId, reason);

        var form = await ValidateFormManagerialAction(formId).ConfigureAwait(false);
        logger.LogDebug("Updating the expense status.");

        form.Status = FormStatus.Rejected;
        repositoryManager.FormRepository.Update(form);

        await repositoryManager.Save().ConfigureAwait(false);

        await formHistoryService.LogFormRejected(formId, DateTime.Now, GetCurrentUserId(), reason);
    }

    public async Task ApproveForm(int formId)
    {
        logger.LogInfo("Approving form {id}", formId);

        var form = await ValidateFormManagerialAction(formId).ConfigureAwait(false);
        logger.LogDebug("Updating the expense status.");

        form.Status = FormStatus.PendingReimbursement;
        repositoryManager.FormRepository.Update(form);

        await repositoryManager.Save().ConfigureAwait(false);

        await formHistoryService.LogFormApproved(formId, DateTime.Now, GetCurrentUserId());
    }


    public async Task ReimburseExpense(int expenseId)
    {
        logger.LogInfo("Reimbursing expense {i}", expenseId);

        var expense = await ValidateExpenseAccountantAction(expenseId).ConfigureAwait(false);
        logger.LogDebug("Updating the expense status.");

        expense.Status = ExpenseStatus.Reimbursed;
        repositoryManager.ExpenseRepository.Update(expense);

        await repositoryManager.Save().ConfigureAwait(false);

        await formHistoryService.LogExpenseReimbursed(expenseId, DateTime.Now, GetCurrentUserId());
    }

    public async Task ReimburseForm(int formId)
    {
        logger.LogInfo("Reimbursing form {i}", formId);

        var form = await ValidateFormManagerialAction(formId).ConfigureAwait(false);
        logger.LogDebug("Updating the form status.");

        form.Status = FormStatus.Reimbursed;
        repositoryManager.FormRepository.Update(form);

        await repositoryManager.Save().ConfigureAwait(false);

        await formHistoryService.LogFormReimbursed(formId, DateTime.Now, GetCurrentUserId());
    }

    public async Task UpdateForm(UpdateExpenseFormModel form, IEnumerable<UpdateExpenseModel> expenses)
    {
        var (formEntity, newExpenses) = await ValidateFormUpdate(form, expenses).ConfigureAwait(false);

        repositoryManager.FormRepository.Update(formEntity);

        foreach(var expense in newExpenses)
        {
            await repositoryManager.ExpenseRepository.Create(expense).ConfigureAwait(false);
        }

        await repositoryManager.Save().ConfigureAwait(false);

        await formHistoryService.LogFormUpdated(form.Id, DateTime.Now, GetCurrentUserId());
    }
}
