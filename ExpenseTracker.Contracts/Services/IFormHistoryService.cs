namespace ExpenseTracker.Contracts.Services;

public interface IFormHistoryService
{
    Task LogFormSubmitted(int formId, DateTimeOffset date, int actorId);

    Task LogFormUpdated(int formId, DateTimeOffset date, int actorId);

    Task LogFormRejected(int formId, DateTimeOffset date, int actorId, string reason);

    Task LogExpenseRejected(int expenseId, DateTimeOffset date, int actorId, string reason);

    Task LogFormCancelled(int formId, DateTimeOffset date, int actorId, string reason);

    Task LogExpenseCancelled(int expenseId, DateTimeOffset date, int actorId, string reason);

    Task LogFormApproved(int formId, DateTimeOffset date, int actorId);
    
    Task LogExpenseApproved(int expense, DateTimeOffset date, int actorId);

    Task LogFormReimbursed(int formId, DateTimeOffset date, int actorId);

    Task LogExpenseReimbursed(int expenseId, DateTimeOffset date, int actorId);
}
