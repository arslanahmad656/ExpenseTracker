using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Repository.Repositories;

public class FormHistoryRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<FormHistory>(repositoryContext), IFormHistoryRepository
{
    public IQueryable<FormHistory> GetHistoriesByFormId(int formId)
        => FindByCondition
        (
            filter: fh => fh.FormId == formId,
            orderBy: fh => fh.OrderBy(fh => fh.RecordedDate)
        );

    public Task Add(FormHistory formHistory) => base.Add(formHistory);
}
