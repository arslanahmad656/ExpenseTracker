using AutoMapper;
using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Contracts.Services;

namespace ExpenseTracker.Services.DataServices;

public class ServiceManager(
    //IRepositoryManager repositoryManager, 
    //IMapper mapper,
    IAuthenticationService authenticationService,
    IFormHistoryService formHistoryService,
    IFormService formService,
    ILoginHistoryService loginHistoryService
    ) : IServiceManager
{
    //private readonly Lazy<IAuthenticationService> accountService = new(() => new AuthenticationService(repositoryManager, mapper));
    //private readonly Lazy<IFormHistoryService> formHistoryService = new(() => new FormHistoryService(repositoryManager));
    //private readonly Lazy<IFormService> formService = new(() => new FormService(repositoryManager));
    //private readonly Lazy<ILoginHistoryService> loginHistoryService = new(() => new LoginHistoryService(repositoryManager));
    //private readonly Lazy<IUserRoleService> userRoleService = new(() => new UserRoleService(repositoryManager));

    public IAuthenticationService AuthenticationService => authenticationService;
    
    public IFormHistoryService FormHistoryService => formHistoryService;
    
    public IFormService FormService => formService;
    
    public ILoginHistoryService LoginHistoryService => loginHistoryService;
}
