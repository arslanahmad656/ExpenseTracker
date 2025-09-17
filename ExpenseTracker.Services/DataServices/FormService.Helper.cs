using ExpenseTracker.Entities.Models;
using ExpenseTracker.Services.Utility;
using ExpenseTracker.Shared.Enums;
using ExpenseTracker.Shared.Models;

namespace ExpenseTracker.Services.DataServices;

public partial class FormService
{
    private Form PrepareFormForCreation(string formTitle, int currencyId, int userId, IEnumerable<CreateExpenseModel> expenses)
    {
        var now = DateTime.Now;
        var form = new Form
        {
            CurrencyId = currencyId,
            Title = formTitle,
            TrackingId = trackingIdGenerator.Generate(),
            Status = FormStatus.PendingApproval,
            FormHistories =
            [
                new()
                {
                    RecordedDate = now,
                    Status = FormStatus.PendingApproval,
                    ActorId = userId
                }
            ],
            Expenses = [.. expenses.Select(e => new Expense
            {
                Amount = e.Amount,
                Date = e.ExpenseDate,
                Details = e.Description,
                Status = ExpenseStatus.PendingApproval,
                TrackingId = trackingIdGenerator.Generate(),
                ExpenseHistories =
                [
                    new()
                    {
                        ActorId = userId,
                        RecordedDate = now,
                        Status = FormStatus.PendingApproval
                    }
                ]
            })],
        };

        return form;
    }

    private async Task Validate(CreateExpenseFormModel expenseForm, IEnumerable<CreateExpenseModel> expenses)
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

    private string GetFormHistoryState(Form form)
    {
        var historyDto = form.ToFormHistoryState();
        var state = serializer.Serialize(historyDto);
        return state;
    }
}
