using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class FormRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<Form>(repositoryContext), IFormRepository
{
}
