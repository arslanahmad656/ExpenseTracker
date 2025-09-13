namespace ExpenseTracker.Contracts.Services;

public interface IServiceManager
{
    IAuthenticationService AuthenticationService { get; }

    IFormHistoryService FormHistoryService { get; }

    IFormService FormService { get; }

    ILoginHistoryService LoginHistoryService { get; }
}
