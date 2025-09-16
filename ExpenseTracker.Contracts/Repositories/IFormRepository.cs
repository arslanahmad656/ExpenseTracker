using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Contracts.Repositories;

public interface IFormRepository
{
    Task Create(Form form);
}
