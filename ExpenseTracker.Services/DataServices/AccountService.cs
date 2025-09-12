using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Contracts.Services;

namespace ExpenseTracker.Services.DataServices;

public class AccountService(IRepositoryManager repositoryManager) : IAccountService
{
    private readonly IRepositoryManager repositoryManager = repositoryManager;
}
