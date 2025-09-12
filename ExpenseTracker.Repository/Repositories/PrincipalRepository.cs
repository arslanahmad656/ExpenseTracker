using ExpenseTracker.Contracts;
using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class PrincipalRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<Principal>(repositoryContext), IPrincipalRepository
{
}
