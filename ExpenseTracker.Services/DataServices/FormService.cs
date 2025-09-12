using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Contracts.Services;

namespace ExpenseTracker.Services.DataServices;

public class FormService(IRepositoryManager repositoryManager) : IFormService
{
    private readonly IRepositoryManager repositoryManager = repositoryManager;
}
