using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class FormStateRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<FormState>(repositoryContext), IFormStateRepository
{
}
