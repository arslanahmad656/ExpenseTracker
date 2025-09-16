using ExpenseTracker.Contracts.Repositories;
using ExpenseTracker.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Repository.Repositories;

public class CurrencyRepository(ExpenseTrackerDbContext repositoryContext) : RepositoryBase<Currency>(repositoryContext), ICurrencyRepository
{
    public Task<List<Currency>> GetAllCurrencies(CancellationToken cancellationToken = default) => FindByCondition().ToListAsync(cancellationToken);

    public Task<Currency?> GetCurrencyByCode(string code, CancellationToken cancellationToken = default) 
        => FindByCondition(filter: c => c.Code == code).SingleOrDefaultAsync(cancellationToken: cancellationToken);
}
