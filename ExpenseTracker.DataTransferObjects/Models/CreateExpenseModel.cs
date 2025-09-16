namespace ExpenseTracker.Shared.Models;

public record CreateExpenseModel
(
    string Description,
    decimal Amount,
    DateTimeOffset ExpenseDate
);

