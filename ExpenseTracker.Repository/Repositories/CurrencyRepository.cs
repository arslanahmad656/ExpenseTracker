using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class CurrencyRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<Currency>(repositoryContext), ICurrencyRepository
{
}
