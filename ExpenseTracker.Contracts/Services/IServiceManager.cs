namespace ExpenseTracker.Contracts.Services;

public interface IServiceManager
{
    IAccountService AccountService { get; }

    IFormHistoryService FormHistoryService { get; }

    IFormService FormService { get; }

    ILoginHistoryService LoginHistoryService { get; }
}
