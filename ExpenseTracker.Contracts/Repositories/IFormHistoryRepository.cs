using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Contracts.Repositories;

public interface IFormHistoryRepository
{
    /// <summary>
    /// Ordered by recorded date in ascending order.
    /// </summary>
    /// <param name="formId"></param>
    /// <returns></returns>
    IQueryable<FormHistory> GetHistoriesByFormId(int formId);

    Task Add(FormHistory formHistory);
}
