namespace ExpenseTracker.Shared.Models;

public record UpdateFormComposite(UpdateExpenseFormModel Form, IEnumerable<UpdateExpenseModel> Expenses);