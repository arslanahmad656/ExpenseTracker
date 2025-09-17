namespace ExpenseTracker.Shared.Models;

public record CreateExpenseFormModel(string Title, string CurrencyCode) : ExpenseFormModelBase(Title, CurrencyCode);
