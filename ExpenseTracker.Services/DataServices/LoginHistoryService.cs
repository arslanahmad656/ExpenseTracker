using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Contracts.Services;

namespace ExpenseTracker.Services.DataServices;

public class LoginHistoryService(IRepositoryManager repositoryManager) : ILoginHistoryService
{
    private readonly IRepositoryManager repositoryManager = repositoryManager;
}
