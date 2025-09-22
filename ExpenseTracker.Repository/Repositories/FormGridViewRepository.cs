using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models.Views;

namespace ExpenseTracker.Repository.Repositories;

public class FormGridViewRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<FormGridView>(repositoryContext), IFormGridViewRepository
{
    public IQueryable<FormGridView> Find(string? orderBy = null, IEnumerable<string>? filters = null)
    {
        return base.FindByConditionDynamic(filters: filters, orderBy: orderBy);
    }
}
