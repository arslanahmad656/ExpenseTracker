using ExpenseTracker.Contracts;
using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class UserRoleRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<UserRole>(repositoryContext), IUserRoleRepository
{
}
