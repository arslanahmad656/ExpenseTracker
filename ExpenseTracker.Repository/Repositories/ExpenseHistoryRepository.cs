using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class ExpenseHistoryRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<ExpenseHistory>(repositoryContext), IExpenseHistoryRepository
{
}
