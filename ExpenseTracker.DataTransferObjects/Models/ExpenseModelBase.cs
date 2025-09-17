namespace ExpenseTracker.Shared.Models;

public abstract record ExpenseModelBase
(
    string Description,
    decimal Amount,
    DateTimeOffset ExpenseDate
);

