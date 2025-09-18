using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Contracts.Repositories;

public interface IExpenseHistoryRepository
{
    /// <summary>
    /// Ordered by recorded date in ascending order.
    /// </summary>
    /// <param name="formId"></param>
    /// <returns></returns>
    IQueryable<ExpenseHistory> GetHistoriesByExpenseId(int expenseId);

    Task Add(ExpenseHistory expenseHistory);
}
