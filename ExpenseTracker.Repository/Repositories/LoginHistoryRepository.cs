using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class LoginHistoryRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<LoginHistory>(repositoryContext), ILoginHistoryRepository
{
}
