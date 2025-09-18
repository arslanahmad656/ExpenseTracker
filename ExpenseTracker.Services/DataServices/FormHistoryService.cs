using ExpenseTracker.Contracts;
using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Contracts.Services;
using ExpenseTracker.Entities.Models;
using ExpenseTracker.Shared.Enums;
using ExpenseTracker.Services.Utility;

namespace ExpenseTracker.Services.DataServices;

public partial class FormHistoryService(
    IRepositoryManager repositoryManager,
    ISerializer serializer
) : IFormHistoryService
{
    public async Task LogExpenseApproved(int expenseId, DateTimeOffset date, int actorId)
    {
        var (_, previousState, currentState) = await Helper.GetExpenseWithStates(repositoryManager, serializer, expenseId).ConfigureAwait(false);

        var entry = new ExpenseHistory
        {
            ActorId = actorId,
            ExpenseId = expenseId,
            RecordedDate = date,
            Status = ExpenseStatus.PendingReimbursement,
            PreviousState = previousState,
            CurrentState = currentState
        };

        await SaveExpenseHistoryEntry(entry).ConfigureAwait(false);
    }

    public async Task LogExpenseCancelled(int expenseId, DateTimeOffset date, int actorId, string reason)
    {
        var (_, previousState, currentState) = await Helper.GetExpenseWithStates(repositoryManager, serializer, expenseId).ConfigureAwait(false);

        var entry = new ExpenseHistory
        {
            ActorId = actorId,
            ExpenseId = expenseId,
            RecordedDate = date,
            Status = ExpenseStatus.Cancelled,
            PreviousState = previousState,
            CurrentState = currentState,
            Note = reason
        };

        await SaveExpenseHistoryEntry(entry).ConfigureAwait(false);
    }

    public async Task LogExpenseReimbursed(int expenseId, DateTimeOffset date, int actorId)
    {
        var (_, previousState, currentState) = await Helper.GetExpenseWithStates(repositoryManager, serializer, expenseId).ConfigureAwait(false);

        var entry = new ExpenseHistory
        {
            ActorId = actorId,
            ExpenseId = expenseId,
            RecordedDate = date,
            Status = ExpenseStatus.Reimbursed,
            PreviousState = previousState,
            CurrentState = currentState
        };

        await SaveExpenseHistoryEntry(entry).ConfigureAwait(false);
    }

    public async Task LogExpenseRejected(int expenseId, DateTimeOffset date, int actorId, string reason)
    {
        var (_, previousState, currentState) = await Helper.GetExpenseWithStates(repositoryManager, serializer, expenseId).ConfigureAwait(false);

        var entry = new ExpenseHistory
        {
            ActorId = actorId,
            ExpenseId = expenseId,
            RecordedDate = date,
            Status = ExpenseStatus.Rejected,
            PreviousState = previousState,
            CurrentState = currentState,
            Note = reason
        };

        await SaveExpenseHistoryEntry(entry).ConfigureAwait(false);
    }

    public async Task LogFormApproved(int formId, DateTimeOffset date, int actorId)
    {
        var (_, previousState, currentState) = await Helper.GetFormWithStates(repositoryManager, serializer, formId).ConfigureAwait(false);

        var entry = new FormHistory
        {
            ActorId = actorId,
            FormId = formId,
            RecordedDate = date,
            Status = FormStatus.PendingReimbursement,
            PreviousState = previousState,
            CurrentState = currentState,
        };

        await SaveFormHistoryEntry(entry).ConfigureAwait(false);
    }

    public async Task LogFormCancelled(int formId, DateTimeOffset date, int actorId, string reason)
    {
        var (_, previousState, currentState) = await Helper.GetFormWithStates(repositoryManager, serializer, formId).ConfigureAwait(false);

        var entry = new FormHistory
        {
            ActorId = actorId,
            FormId = formId,
            RecordedDate = date,
            Status = FormStatus.Cancelled,
            PreviousState = previousState,
            CurrentState = currentState,
            Note = reason
        };

        await SaveFormHistoryEntry(entry).ConfigureAwait(false);
    }

    public async Task LogFormReimbursed(int formId, DateTimeOffset date, int actorId)
    {
        var (_, previousState, currentState) = await Helper.GetFormWithStates(repositoryManager, serializer, formId).ConfigureAwait(false);

        var entry = new FormHistory
        {
            ActorId = actorId,
            FormId = formId,
            RecordedDate = date,
            Status = FormStatus.Reimbursed,
            PreviousState = previousState,
            CurrentState = currentState
        };

        await SaveFormHistoryEntry(entry).ConfigureAwait(false);
    }

    public async Task LogFormRejected(int formId, DateTimeOffset date, int actorId, string reason)
    {
        var (_, previousState, currentState) = await Helper.GetFormWithStates(repositoryManager, serializer, formId).ConfigureAwait(false);

        var entry = new FormHistory
        {
            ActorId = actorId,
            FormId = formId,
            RecordedDate = date,
            Status = FormStatus.Rejected,
            PreviousState = previousState,
            CurrentState = currentState,
            Note = reason
        };

        await SaveFormHistoryEntry(entry).ConfigureAwait(false);
    }

    public async Task LogFormSubmitted(int formId, DateTimeOffset date, int actorId)
    {
        var (form, previousState, currentState) = await Helper.GetFormWithStates(repositoryManager, serializer, formId).ConfigureAwait(false);

        var formHistoryEntry = new FormHistory
        {
            ActorId = actorId,
            FormId = formId,
            RecordedDate = date,
            Status = FormStatus.PendingApproval,
            PreviousState = previousState,
            CurrentState = currentState
        };

        //await SaveFormHistoryEntry(entry).ConfigureAwait(false);
        await repositoryManager.FormHistoryRepository.Add(formHistoryEntry).ConfigureAwait(false);

        foreach (var expense in form.Expenses)
        {
            var expenseState = await Helper.GetExpenseWithStates(repositoryManager, serializer, expense.Id).ConfigureAwait(false);
            var entry = new ExpenseHistory
            {
                ActorId = actorId,
                ExpenseId = expense.Id,
                RecordedDate = date,
                Status = ExpenseStatus.PendingApproval,
                PreviousState = expenseState.previousState,
                CurrentState = expenseState.currentState
            };

            await repositoryManager.ExpenseHistoryRepository.Add(entry).ConfigureAwait(false);
        }

        await repositoryManager.Save().ConfigureAwait(false);
    }

    public async Task LogFormUpdated(int formId, DateTimeOffset date, int actorId)
    {
        var (form, formPreviousState, formCurrentState) = await Helper.GetFormWithStates(repositoryManager, serializer, formId).ConfigureAwait(false);
        var formChanged = false;

        var previousStateValues = formPreviousState is not null ? serializer.Deserialize<Dictionary<string, object?>>(formPreviousState) : null;

        if (previousStateValues is null)
        {
            formChanged = true;
        }
        else
        {
            var currentStateValues = serializer.Deserialize<Dictionary<string, object?>>(formCurrentState);

            var changedKeys = Helper.FindChangedKeys(previousStateValues, currentStateValues!);

            if (changedKeys.Count > 0)
            {
                formChanged = true;
            }
        }

        var formEntry = formChanged ? new FormHistory
        {
            ActorId = actorId,
            FormId = formId,
            RecordedDate = date,
            Status = FormStatus.PendingApproval,
            PreviousState = formPreviousState,
            CurrentState = formCurrentState
        } : null;

        if (formEntry is not null)
        {
            await repositoryManager.FormHistoryRepository.Add(formEntry).ConfigureAwait(false);
        }

        List<(int ExpenseId, string PreviousState, string CurrentState)> changedExpenses = [];
        foreach (var expense in form.Expenses)
        {
            var (_, previousState, currentState) = await Helper.GetExpenseWithStates(repositoryManager, serializer, expense.Id).ConfigureAwait(false);
            var previousValues = previousState is null ? null : serializer.Deserialize<Dictionary<string, object?>>(previousState);
            if (previousState is null)
            {
                changedExpenses.Add((expense.Id, previousState!, currentState!));    // definetly changed
                continue;
            }

            var currentValues = serializer.Deserialize<Dictionary<string, object?>>(currentState);

            var changedKeys = Helper.FindChangedKeys(previousValues!, currentValues!);

            if (changedKeys.Count > 0)
            {
                changedExpenses.Add((expense.Id, previousState!, currentState!));
            }
        }

        foreach (var (ExpenseId, PreviousState, CurrentState) in changedExpenses)
        {
            var entry = new ExpenseHistory
            {
                ActorId = actorId,
                ExpenseId = ExpenseId,
                RecordedDate = date,
                Status = ExpenseStatus.PendingApproval,
                PreviousState = PreviousState,
                CurrentState = CurrentState
            };

            await repositoryManager.ExpenseHistoryRepository.Add(entry).ConfigureAwait(false);
        }

        await repositoryManager.Save().ConfigureAwait(false);
    }
}
