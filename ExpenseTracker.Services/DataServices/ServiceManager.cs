using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Contracts.Services;

namespace ExpenseTracker.Services.DataServices;

public class ServiceManager(IRepositoryManager repositoryManager) : IServiceManager
{
    private readonly IRepositoryManager repositoryManager = repositoryManager;
    private readonly Lazy<IAccountService> _accountService = new(() => new AccountService(repositoryManager));
    private readonly Lazy<IFormHistoryService> _formHistoryService = new(() => new FormHistoryService(repositoryManager));
    private readonly Lazy<IFormService> _formService = new(() => new FormService(repositoryManager));
    private readonly Lazy<ILoginHistoryService> _loginHistoryService = new(() => new LoginHistoryService(repositoryManager));
    
    public IAccountService AccountService => _accountService.Value;
    
    public IFormHistoryService FormHistoryService => _formHistoryService.Value;
    
    public IFormService FormService => _formService.Value;
    
    public ILoginHistoryService LoginHistoryService => _loginHistoryService.Value;
}
