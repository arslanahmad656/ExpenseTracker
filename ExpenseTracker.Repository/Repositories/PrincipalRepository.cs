using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repository.Repositories;

public class PrincipalRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<Principal>(repositoryContext), IPrincipalRepository
{
    public Task<Principal> GetActiveUser(string username)
    {
        var user = FindByCondition(
            filter: p => p.Username == username && p.IsActive,
            include: p => p.Include(p => p.UserRoles).ThenInclude(ur => ur.Role)).SingleAsync();

        return user;
    }
}
