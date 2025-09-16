namespace ExpenseTracker.Contracts.Repositories;

public interface IRepositoryManager
{
    ICurrencyRepository CurrencyRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    IExpenseRepository ExpenseRepository { get; }
    IExpenseHistoryRepository ExpenseHistoryRepository { get; }
    IFormRepository FormRepository { get; }
    IFormHistoryRepository FormHistoryRepository { get; }
    IPrincipalRepository PrincipalRepository { get; }
    IRoleRepository RoleRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
    ILoginHistoryRepository LoginHistoryRepository { get; }
    Task Save();

}
