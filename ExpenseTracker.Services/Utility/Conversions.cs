using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Services.Utility;

public static class Conversions
{
    public static FormHistoryState ToFormHistoryState(this Form form) 
        => new(form.Title, form.Currency?.Code, form.Status);

    public static ExpenseHistoryState ToExpenseHistoryState(this Expense expense)
        => new(expense.Details, expense.Amount, expense.Date, expense.Status);
}
