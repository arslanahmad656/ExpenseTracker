namespace ExpenseTracker.Shared.Models;

public record CreateExpenseForm(CreateExpenseFormModel Form, IEnumerable<CreateExpenseModel> Expenses);
