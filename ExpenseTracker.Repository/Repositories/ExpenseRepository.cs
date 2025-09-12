using ExpenseTracker.Contracts;
using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class ExpenseRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<Expense>(repositoryContext), IExpenseRepository
{
}
