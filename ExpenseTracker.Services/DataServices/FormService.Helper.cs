using System.ComponentModel.DataAnnotations;
using System.Net.Http.Headers;
using System.Net.Mail;
using ExpenseTracker.Entities.Models;
using ExpenseTracker.Services.Utility;
using ExpenseTracker.Shared.DataTransferObjects;
using ExpenseTracker.Shared.Enums;
using ExpenseTracker.Shared.Models;

namespace ExpenseTracker.Services.DataServices;

public partial class FormService
{
    private Form PrepareFormForCreation(string formTitle, int currencyId, DateTimeOffset now, IEnumerable<CreateExpenseModel> expenses)
    {
        var form = new Form
        {
            CurrencyId = currencyId,
            Title = formTitle,
            TrackingId = trackingIdGenerator.Generate(),
            Status = FormStatus.PendingApproval,
            Expenses = [.. expenses.Select(e => new Expense
            {
                Amount = e.Amount,
                Date = e.ExpenseDate,
                Details = e.Description,
                Status = ExpenseStatus.PendingApproval,
                TrackingId = trackingIdGenerator.Generate()
            })],
        };

        return form;
    }

    private async Task ValidateForCreation(CreateExpenseFormModel expenseForm, IEnumerable<CreateExpenseModel> expenses)
    {
        var currency = await repositoryManager.CurrencyRepository.GetCurrencyByCode(expenseForm.CurrencyCode).ConfigureAwait(false);
        if (currency is null)
        {
            logger.LogWarn($"Currency is null.");
            throw new InvalidOperationException($"Cannot create an expense form without a valid currency.");
        }

        ValidateExpenses(expenses);
    }

    private void ValidateExpenses(IEnumerable<CreateExpenseModel> expenses)
    {
        bool expenseExists = false;

        foreach (var expense in expenses)
        {
            expenseExists = true;
            if (expense.ExpenseDate >= DateTime.Now)
            {
                logger.LogWarn($"Expense date cannot be from futrue.");
                throw new InvalidOperationException($"Expense date {expenses} is not allowed since it's from future.");
            }
        }

        if (!expenseExists)
        {
            logger.LogWarn($"Expenses list is empty.");
            throw new InvalidOperationException($"Cannot create an expense form without any expenses.");
        }
    }

    private async Task<Form> GetFormOwnedByCurrentUser(int formId)
    {
        logger.LogDebug("Getting current principal id.");
        var currentPrincipalId = authenticationService.GetCurrentUserId() ?? throw new UnauthorizedAccessException();
        logger.LogDebug("Principal ID: {principalId}", currentPrincipalId);

        logger.LogDebug("Call to the db.");
        var form = await repositoryManager.FormRepository.GetById(formId, int.Parse(currentPrincipalId)).ConfigureAwait(false);
        if (form == null)
        {
            logger.LogWarn($"Form not found with id {formId}.");

            throw new ApplicationException($"Form with id {formId} not found for the user id {currentPrincipalId}.");
        }

        return form;
    }

    private async Task<Form> GetFormManagedByCurrentUser(int formId)
    {
        logger.LogDebug("Getting current principal id.");
        var currentPrincipalId = GetCurrentUserId();
        logger.LogDebug("Principal ID: {principalId}", currentPrincipalId);

        logger.LogDebug("Call to the db.");
        var form = await repositoryManager.FormRepository.GetById(formId).ConfigureAwait(false);
        if (form == null)
        {
            logger.LogWarn($"Form not found with id {formId}.");

            throw new ApplicationException($"Form with id {formId} not found for the user id {currentPrincipalId}.");
        }

        var formManagerId = form.FormHistories.First().Actor.ManagerId!.Value;
        if (formManagerId != currentPrincipalId)
        {
            throw new UnauthorizedAccessException("A manager can only treat the form of one of its direct reports.");
        }

        return form;
    }

    private async Task<Form> GetFormForAccountant(int formId)
    {
        var form = await repositoryManager.FormRepository.GetById(formId).ConfigureAwait(false);
        
        if (form.Status != FormStatus.PendingReimbursement)
        {
            throw new Exception($"Accountant can only treat the form which is pending reimbursement.");
        }

        return form;
    }

    private async Task<Form> GetFormForAdmin(int formId)
    {
        var form = await repositoryManager.FormRepository.GetById(formId).ConfigureAwait(false);

        return form;
    }

    //private async Task<Expense> ValidateExpenseAccountantAction(int expenseId)
    //{
    //    logger.LogDebug("Checking if the user is an accountant.");
    //    if (!authenticationService.IsCurrentUserInRole(BuiltInRole.Accountant.ToString()))
    //    {
    //        logger.LogWarn("Invalid role found for the accountant action.");

    //        throw new UnauthorizedAccessException($"Only an accountant can perform the reimbursement.");
    //    }

    //    var expense = await repositoryManager.ExpenseRepository.GetByIdWithNavigations(expenseId).ConfigureAwait(false);

    //    ValidateAgainstStatus((int)expense.Status, [(int)ExpenseStatus.PendingReimbursement, (int)ExpenseStatus.PendingApproval]);

    //    var formStatus = expense.Form.Status;
    //    ValidateAgainstStatus((int)formStatus, [(int)FormStatus.PendingReimbursement]);

    //    logger.LogDebug("Accountant action validated.");

    //    return expense;
    //}

    private async Task<(Form Form, List<Expense> ExpensesToAdd)> ValidateFormUpdate(UpdateExpenseFormModel formModel, IEnumerable<UpdateExpenseModel> expenses)
    {
        var form = await repositoryManager.FormRepository.GetByIdWithNavigations(formModel.Id).ConfigureAwait(false)
            ?? throw new ApplicationException($"Form with id {formModel.Id} not found.");

        var existingFormState = new Dictionary<string, object?>
        {
            ["Currency"] = form.Currency.Code,
            ["Title"] = form.Title
        };

        var changedFormState = new Dictionary<string, object?>
        {
            ["Currency"] = formModel.CurrencyCode,
            ["Title"] = formModel.Title,
        };
        

        var changedKeys = Helper.FindChangedKeys(existingFormState, changedFormState);
        var formChanged = changedKeys.Count > 0;

        //var formWithStates = Helper.GetFormWithStates(repositoryManager, serializer, formModel.Id);

        ValidateFormSubmitterId(form);
        ValidateAgainstStatus((int)form.Status, [(int)FormStatus.PendingApproval, (int)FormStatus.Rejected]);

        bool anyChange = false;
        if (formChanged)
        {
            form.Title = formModel.Title;
            form.CurrencyId = (await repositoryManager.CurrencyRepository.GetCurrencyByCode(formModel.CurrencyCode).ConfigureAwait(false)).Id;
            anyChange = true;
        }

        repositoryManager.FormRepository.Update(form);

        var expensesToId = form.Expenses.ToDictionary(e => e.Id, e => e);
        var addedExpenses = new List<Expense>();

        foreach (var expenseModel in expenses)
        {
            if (expenseModel.Id is 0)
            {
                anyChange = true;
                addedExpenses.Add(new()
                {
                    Amount = expenseModel.Amount,
                    Date = expenseModel.ExpenseDate,
                    Details = expenseModel.Description,
                    FormId = form.Id,
                    Status = ExpenseStatus.PendingApproval,
                    TrackingId = trackingIdGenerator.Generate()
                });
            }
            else
            {
                var expense = expensesToId[expenseModel.Id];

                var existingExpenseState = new Dictionary<string, object?>
                {
                    ["Details"] = expense.Details,
                    ["Amount"] = expense.Amount,
                    ["Date"] = expense.Date,
                };

                var currentExpenseState = new Dictionary<string, object?>
                {
                    ["Details"] = expenseModel.Description,
                    ["Amount"] = expenseModel.Amount,
                    ["Date"] = expenseModel.ExpenseDate,
                };

                var changedExpenseKeys = Helper.FindChangedKeys(existingExpenseState, currentExpenseState);
                if (changedExpenseKeys.Count is 0)
                {
                    continue;   // no change detected
                }

                anyChange = true;
                ValidateAgainstStatus((int)expense.Status, [(int)FormStatus.PendingApproval, (int)FormStatus.Rejected]);
                (expense.Details, expense.Amount, expense.Date, expense.Status) = (expenseModel.Description, expenseModel.Amount, expenseModel.ExpenseDate, ExpenseStatus.PendingApproval);

                repositoryManager.ExpenseRepository.Update(expense);
            }
        }

        if (anyChange)
        {
            form.Status = FormStatus.PendingApproval;   // for any kind of change, the form state needs to be put back in PendingApproval
        }

        return (form, addedExpenses);
    }

    private async Task<Form> ValidateFormAccountantAction(int formId)
    {
        logger.LogDebug("Checking if the user is an accountant.");
        if (!authenticationService.IsCurrentUserInRole(BuiltInRole.Accountant.ToString()))
        {
            logger.LogWarn("Invalid role found for the accountant action.");

            throw new UnauthorizedAccessException($"Only an accountant can perform the reimbursement.");
        }

        var form = await repositoryManager.FormRepository.GetByIdWithNavigations(formId).ConfigureAwait(false);

        ValidateAgainstStatus((int)form.Status, [(int)FormStatus.PendingReimbursement]);

        logger.LogDebug("Accountant action validated.");

        return form;
    }

    private async Task<Expense> ValidateExpenseCancellation(int expenseId)
    {
        logger.LogDebug("Getting expense by id {id}", expenseId);
        var expense = await repositoryManager.ExpenseRepository.GetByIdWithNavigations(expenseId).ConfigureAwait(false);

        // 1: user should only be able to update the expense which he submitted.
        ValidateFormSubmitterId(expense.Form);

        // 2: state check - expense
        //var expenseStatus = GetLatestState(expense);
        var expenseStatus = expense.Status;
        ValidateAgainstStatus((int)expenseStatus, [(int)ExpenseStatus.PendingApproval, (int)ExpenseStatus.PendingReimbursement, (int)ExpenseStatus.Rejected]);

        // 3: state check - form
        //var formStatus = GetLatestState(expense.Form);
        var formStatus = expense.Form.Status;
        ValidateAgainstStatus((int)formStatus, [(int)FormStatus.PendingApproval, (int)FormStatus.PendingReimbursement, (int)FormStatus.Rejected]);

        return expense;
    }

    private async Task<Form> ValidateFormCancellation(int formId)
    {
        logger.LogDebug("Getting form by id {formId}", formId);
        var form = await repositoryManager.FormRepository.GetByIdWithNavigations(formId).ConfigureAwait(false) 
            ?? throw new ApplicationException($"Form with id {formId} not found.");

        ValidateFormSubmitterId(form);

        var formStatus = form.Status;
        ValidateAgainstStatus((int)formStatus, [(int)ExpenseStatus.PendingApproval, (int)ExpenseStatus.PendingReimbursement, (int)ExpenseStatus.Rejected]);

        return form;
    }

    //private async Task<Expense> ValidateExpenseManagerialAction(int expenseId)
    //{
    //    logger.LogDebug("Validating whether manager can update expense {id}", expenseId);

    //    var expense = await repositoryManager.ExpenseRepository.GetByIdWithNavigations(expenseId).ConfigureAwait(false);

    //    // 1: user should only be able to update the expense of the form submitted by its direct report.
    //    ValidateFormManagerId(expense.Form);

    //    // 2: state check - expense
    //    var expenseStatus = expense.Status;
    //    ValidateAgainstStatus((int)expenseStatus, [(int)ExpenseStatus.PendingApproval]);

    //    // 3: state check - form
    //    var formStatus = expense.Form.Status;
    //    ValidateAgainstStatus((int)formStatus, [(int)FormStatus.PendingApproval]);

    //    return expense;
    //}

    private async Task<Form> ValidateFormManagerialAction(int formId)
    {
        logger.LogDebug("Validating whether manager can update form {id}", formId);

        var form = await repositoryManager.FormRepository.GetByIdWithNavigations(formId).ConfigureAwait(false)
            ?? throw new ApplicationException($"Form with id {formId} not found.");

        ValidateFormManagerId(form);

        var formStatus = form.Status;
        ValidateAgainstStatus((int)formStatus, [(int)FormStatus.PendingApproval]);

        return form;
    }

    private void ValidateFormSubmitterId(Form form)
    {
        var currentUserId = GetCurrentUserId();
        var formSubmitterId = GetFormSubmitterId(form);
        logger.LogDebug("Checking if the form was submitted by the current user.");
        if (formSubmitterId != currentUserId)
        {
            logger.LogWarn("User id {userId} trying to update form with id {formId}. Restricted.");
            throw new UnauthorizedAccessException($"Form can only be updated by the employee who submitted it.");
        }
    }

    private void ValidateFormManagerId(Form form)
    {
        var currentUserId = GetCurrentUserId();
        var formManagerId = GetFormManagerId(form);
        logger.LogDebug("Checking if the submitter of the form {id} is a direct report of the current user {managerId}", form.Id, currentUserId);
        if (formManagerId != currentUserId)
        {
            logger.LogWarn("User id {userId} trying to update form with id {formId}. Restricted.");
            throw new UnauthorizedAccessException($"Form can only be updated by the employee who submitted it.");
        }
    }

    private void ValidateAgainstStatus(int status, IEnumerable<int> validStates)
    {
        logger.LogDebug("Validating against state.");
        if (!validStates.Contains(status))
        {
            logger.LogWarn("Invalid state.");

            throw new InvalidOperationException($"Cannot perform the update because of the states conflict.");
        }
    }

    private int GetFormSubmitterId(Form form)
    {
        logger.LogDebug("Trying to determine the form submitter id.");
        var historyEntry = form.FormHistories.OrderBy(h => h.RecordedDate).First();
        var submitterId = historyEntry.Actor.Id;
        logger.LogDebug("Form submitter id: {@id}", submitterId);

        return submitterId;
    }

    private int GetFormManagerId(Form form)
    {
        logger.LogDebug("Determing the manager id of the form {id}", form.Id);

        var submissionHistory = form.FormHistories.OrderBy(h => h.RecordedDate).First();
        var managerId = submissionHistory.Actor.ManagerId;

        if (managerId is null)
        {
            logger.LogWarn("Could not determine the manager id for the form {id}", form.Id);

            throw new ApplicationException($"Could not determine the manager id for the form with id {form.Id}.");
        }

        logger.LogDebug("Manager id: {managerId}", managerId);

        return managerId.Value;
    }

    //private ExpenseStatus GetLatestState(Expense form)
    //{
    //    logger.LogDebug("Getting latest state of the expense {id}", form.Id);
    //    var historyEntry = form.ExpenseHistories.OrderBy(h => h.RecordedDate).Last();
    //    logger.LogDebug("Latest state: {state}", historyEntry.Status);
    //    return historyEntry.Status;
    //}

    //private FormStatus GetLatestState(Form form)
    //{
    //    logger.LogDebug("Getting latest state of the form {id}", form.Id);
    //    var historyEntry = form.FormHistories.OrderBy(h => h.RecordedDate).Last();
    //    logger.LogDebug("Latest state: {state}", historyEntry.Status);
    //    return historyEntry.Status;
    //}

    private int GetCurrentUserId()
    {
        logger.LogDebug("Trying to determine the current principal.");
        var currentUserId = int.Parse(authenticationService.GetCurrentUserId() ?? throw new UnauthorizedAccessException());
        logger.LogDebug("Principal id {principalId}", currentUserId);

        return currentUserId;
    }

    private FormDto GetFormDetailsForCurrentUser(Form form)
    {
        var formDto = mapper.Map<FormDto>(form);
        formDto = formDto with
        {
            LastUpdatedOn = form.FormHistories.MaxBy(fh => fh.RecordedDate)?.RecordedDate ?? throw new ApplicationException($"Could not determine the last update date f the form {formDto.Id}."),
            RejectionReason = form.Status == FormStatus.Rejected ? form.FormHistories.MaxBy(fh => fh.RecordedDate)!.Note : string.Empty,
            Expenses = [.. form.Expenses.Select(e => mapper.Map<Expense, ExpenseDto>(e) with
            {
                LastUpdatedOn = e.ExpenseHistories.MaxBy(eh => eh.RecordedDate)?.RecordedDate ?? throw new ApplicationException($"Could not determine the last update date of the expense {e.Id}."),
                RejectionReason = e.Status == ExpenseStatus.Rejected ? e.ExpenseHistories.MaxBy(eh => eh.RecordedDate)!.Note : string.Empty,
            })]
        };

        return formDto;
    }

    private void UpdateExpenseStates(IEnumerable<Expense> expenses, ExpenseStatus newState)
    {
        foreach (var expense in expenses)
        {
            if (expense.Status is not ExpenseStatus.Cancelled)
            {
                expense.Status = newState;
            }

            repositoryManager.ExpenseRepository.Update(expense);
        }
    }
}
