using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repository.Repositories;

public class ExpenseRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<Expense>(repositoryContext), IExpenseRepository
{
    public Task Create(Expense expense) => Add(expense);

    public Task<Expense> GetByIdWithNavigations(int expenseId)
        => FindByCondition
        (
            filter: e => e.Id == expenseId,
            include: e => e
                .Include(e => e.ExpenseHistories)
                .Include(e => e.Form)
                .ThenInclude(f => f.FormHistories)
                .ThenInclude(fh => fh.Actor)
        ).SingleAsync();

    public new void Update(Expense expense)
        => base.Update(expense);
}
