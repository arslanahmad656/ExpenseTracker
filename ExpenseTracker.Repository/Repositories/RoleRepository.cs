using ExpenseTracker.Contracts;
using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class RoleRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<Role>(repositoryContext), IRoleRepository
{
}
