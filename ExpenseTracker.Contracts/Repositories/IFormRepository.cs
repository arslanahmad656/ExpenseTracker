using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Contracts.Repositories;

public interface IFormRepository
{
    Task Create(Form form);

    Task<Form> GetById(int id, int principalId);
    
    Task<Form> GetByIdWithNavigations(int id);

    void Update(Form form);
}
