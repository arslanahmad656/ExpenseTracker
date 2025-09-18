namespace ExpenseTracker.Shared.Models;

public record UpdateExpenseModel
(
    int Id,
    string Description,
    decimal Amount,
    DateTimeOffset ExpenseDate
) : ExpenseModelBase(Description, Amount, ExpenseDate);

