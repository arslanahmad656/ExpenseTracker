using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class ExpenseHistoryRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<ExpenseHistory>(repositoryContext), IExpenseHistoryRepository
{
    public IQueryable<ExpenseHistory> GetHistoriesByExpenseId(int expenseId)
        => FindByCondition
        (
            filter: eh => eh.ExpenseId == expenseId,
            orderBy: eh => eh.OrderBy(eh => eh.RecordedDate)
        );

    public Task Add(ExpenseHistory expenseHistory)
        => base.Add(expenseHistory);
}
