using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Entities.Models;

public class ExpenseState
{
    public int Id { get; set; }

    [Required]
    public ExpenseStatus State { get; set; }

    public ICollection<Expense> Expenses { get; set; }
}
