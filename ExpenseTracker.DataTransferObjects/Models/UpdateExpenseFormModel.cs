namespace ExpenseTracker.Shared.Models;

public record UpdateExpenseFormModel(int Id, string Title, string CurrencyCode) : ExpenseFormModelBase(Title, CurrencyCode);