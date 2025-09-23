using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;
using ExpenseTracker.Shared.Enums;
using Microsoft.EntityFrameworkCore;

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

    public IQueryable<Form> GetAllSubmittedByUser(int principalId) => FindByCondition()
        .Include(fh => fh.Form)
        .Where(fh => (fh.Status == FormStatus.PendingApproval || fh.Status == FormStatus.Rejected) && fh.ActorId == principalId)
        .Select(fh => fh.Form)
        .GroupBy(f => f.Id)
        .Select(g => g.First());

    public IQueryable<Form> GetAllManagedByUser(int principalId) => FindByCondition()
        .Include(fh => fh.Form)
        .Include(fh => fh.Actor)
        .Where(fh => (fh.Status == FormStatus.PendingApproval) && fh.Actor.ManagerId == principalId)
        .Select(fh => fh.Form)
        .GroupBy(f => f.Id)
        .Select(g => g.First());
}
