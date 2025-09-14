using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class ExpenseStateRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<ExpenseState>(repositoryContext), IExpenseStateRepository
{
}
