using ExpenseTracker.Shared.Models;

namespace ExpenseTracker.Contracts.Services;

public interface IFormService
{
    Task<int> SubmitExpenseForm(CreateExpenseFormModel expenseForm);
}
