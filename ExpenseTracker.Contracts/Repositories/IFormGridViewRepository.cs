using ExpenseTracker.Entities.Models.Views;

namespace ExpenseTracker.Contracts.Repositories;

public interface IFormGridViewRepository
{
    IQueryable<FormGridView> Find(string? orderBy = null, string? sortOrder = null, IEnumerable<string>? filters = null);
}
