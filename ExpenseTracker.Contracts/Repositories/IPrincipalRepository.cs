using ExpenseTracker.Entities.Models;
using ExpenseTracker.Shared.DataTransferObjects;

namespace ExpenseTracker.Contracts.Repositories;

public interface IPrincipalRepository
{
    Task<Principal> GetActiveUser(string username);
}
