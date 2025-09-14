using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class FormHistoryRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<FormHistory>(repositoryContext), IFormHistoryRepository
{
}
