namespace ExpenseTracker.Shared.Models;

public record UpdateExpenseModel
(
    string Description,
    decimal Amount,
    DateTimeOffset ExpenseDate
) : ExpenseModelBase(Description, Amount, ExpenseDate);

