namespace ExpenseTracker.Shared.Models;

public record UpdateExpenseFormModel(string Title, string CurrencyCode) : ExpenseFormModelBase(Title, CurrencyCode);