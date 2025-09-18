using ExpenseTracker.Shared.DataTransferObjects;
using ExpenseTracker.Shared.Models;

namespace ExpenseTracker.Contracts.Services;

public interface IFormService
{
    Task<int> SubmitExpenseForm(CreateExpenseFormModel expenseForm, IEnumerable<CreateExpenseModel> expenses);

    Task<FormDto> GetFormDetailedOwnedByCurrentUser(int formId);

    Task CancelExpense(int expenseId, string reason);

    Task CancelForm(int formId, string reason);

    Task RejectExpense(int expenseId, string reason);

    Task ApproveExpense(int expenseId);

    Task RejectForm(int formId, string reason);

    Task ApproveForm(int formId);

    Task ReimburseExpense(int expenseId);

    Task ReimburseForm(int formId);

    Task UpdateForm(UpdateExpenseFormModel form, IEnumerable<UpdateExpenseModel> expenses);
}
