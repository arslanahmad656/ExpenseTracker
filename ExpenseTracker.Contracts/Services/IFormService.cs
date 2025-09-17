using ExpenseTracker.Shared.DataTransferObjects;
using ExpenseTracker.Shared.Models;

namespace ExpenseTracker.Contracts.Services;

public interface IFormService
{
    Task<int> SubmitExpenseForm(CreateExpenseFormModel expenseForm, IEnumerable<CreateExpenseModel> expenses);

    Task<FormDto> GetFormDetailedOwnedByCurrentUser(int formId);
}
