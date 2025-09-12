using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Contracts;

namespace ExpenseTracker.Entities.Models;

public class ExpenseState : IEntity
{
    public int Id { get; set; }

    [Required]
    public ExpenseStatus State { get; set; }

    public ICollection<Expense> Expenses { get; set; }
}
