using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Contracts.Services;

namespace ExpenseTracker.Services.DataServices;

public class FormHistoryService(IRepositoryManager repositoryManager) : IFormHistoryService
{
    private readonly IRepositoryManager repositoryManager = repositoryManager;
}
