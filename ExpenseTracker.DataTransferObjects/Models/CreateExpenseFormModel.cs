namespace ExpenseTracker.Shared.Models;

public record CreateExpenseFormModel(string Title, string CurrencyCode, List<CreateExpenseModel> Expenses);
