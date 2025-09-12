using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class EmployeeRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<Employee>(repositoryContext), IEmployeeRepository
{
}
