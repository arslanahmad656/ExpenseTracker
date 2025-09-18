using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;
using ExpenseTracker.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repository.Repositories;

public class FormRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<Form>(repositoryContext), IFormRepository
{
    public Task Create(Form form) => Add(form);

    public Task<Form> GetById(int id, int submitterId) => FindByCondition(
        filter: f => f.Id == id && f.FormHistories.Single(fh => fh.Status == FormStatus.PendingApproval).ActorId == submitterId, 
        include: f => f
            .Include(f => f.Currency)
            .Include(f => f.FormHistories)
            .Include(f => f.Expenses)
            .ThenInclude(e => e.ExpenseHistories))
        .SingleAsync();

    public Task<Form> GetById(int id) => FindByCondition(
        filter: f => f.Id == id,
        include: f => f
            .Include(f => f.Currency)
            .Include(f => f.FormHistories)
            .ThenInclude(fh => fh.Actor)
            .Include(f => f.Expenses)
            .ThenInclude(e => e.ExpenseHistories))
        .SingleAsync();

    public Task<Form> GetByIdWithNavigations(int id) => FindByCondition(
        filter: f => f.Id == id,
        include: f => f
            .Include(f => f.Currency)
            .Include(f => f.FormHistories)
            .ThenInclude(fh => fh.Actor)
            .Include(f => f.Expenses)
            .ThenInclude(e => e.ExpenseHistories))
        .SingleAsync();

    public new void Update(Form form) => base.Update(form);
}
