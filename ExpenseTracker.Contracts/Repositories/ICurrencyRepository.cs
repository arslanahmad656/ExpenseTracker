using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Contracts.Repositories;

public interface ICurrencyRepository
{
    Task<List<Currency>> GetAllCurrencies(CancellationToken cancellationToken = default);

    Task<Currency> GetCurrencyByCode(string code, CancellationToken cancellationToken = default);
}
