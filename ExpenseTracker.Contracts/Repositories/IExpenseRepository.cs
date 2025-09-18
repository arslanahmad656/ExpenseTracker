using ExpenseTracker.Entities.Models;

namespace ExpenseTracker.Contracts.Repositories;

public interface IExpenseRepository
{
    Task<Expense> GetByIdWithNavigations(int expenseId);

    void Update(Expense expense);

    Task Create(Expense expense);
}
