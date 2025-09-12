using ExpenseTracker.Contracts.Repositories;

namespace ExpenseTracker.Repository.Repositories;

public class RepositoryManager(ExpenseTrackerDbContext context) : IRepositoryManager
{
    private readonly ExpenseTrackerDbContext _context = context;
    private readonly Lazy<ICurrencyRepository> _currencyRepository = new(() => new CurrencyRepository(context));
    private readonly Lazy<IEmployeeRepository> _employeeRepository = new(() => new EmployeeRepository(context));
    private readonly Lazy<IExpenseRepository> _expenseRepository = new(() => new ExpenseRepository(context));
    private readonly Lazy<IExpenseStateRepository> _expenseStateRepository = new(() => new ExpenseStateRepository(context));
    private readonly Lazy<IExpenseHistoryRepository> _expenseHistoryRepository = new(() => new ExpenseHistoryRepository(context));
    private readonly Lazy<IFormRepository> _formRepository = new(() => new FormRepository(context));
    private readonly Lazy<IFormStateRepository> _formStateRepository = new(() => new FormStateRepository(context));
    private readonly Lazy<IFormHistoryRepository> _formHistoryRepository = new(() => new FormHistoryRepository(context));
    private readonly Lazy<IPrincipalRepository> _principalRepository = new(() => new PrincipalRepository(context));
    private readonly Lazy<IRoleRepository> _roleRepository = new(() => new RoleRepository(context));
    private readonly Lazy<IUserRoleRepository> _userRoleRepository = new(() => new UserRoleRepository(context));
    private readonly Lazy<ILoginHistoryRepository> _loginHistoryRepository = new(() => new LoginHistoryRepository(context));

    public ICurrencyRepository CurrencyRepository => _currencyRepository.Value;

    public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

    public IExpenseRepository ExpenseRepository => _expenseRepository.Value;

    public IExpenseStateRepository ExpenseStateRepository => _expenseStateRepository.Value;

    public IExpenseHistoryRepository ExpenseHistoryRepository => _expenseHistoryRepository.Value;

    public IFormRepository FormRepository => _formRepository.Value;

    public IFormStateRepository FormStateRepository => _formStateRepository.Value;

    public IFormHistoryRepository FormHistoryRepository => _formHistoryRepository.Value;

    public IPrincipalRepository PrincipalRepository => _principalRepository.Value;

    public IRoleRepository RoleRepository => _roleRepository.Value;

    public IUserRoleRepository UserRoleRepository => _userRoleRepository.Value;

    public ILoginHistoryRepository LoginHistoryRepository => _loginHistoryRepository.Value;
    
    public Task Save() => _context.SaveChangesAsync();
}
