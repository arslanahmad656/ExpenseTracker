using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Shared.Contracts;

namespace ExpenseTracker.Entities.Models;

public class ExpenseHistory : IEntity
{
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Expense))]
    public int ExpenseId { get; set; }

    [Required]
    public DateTimeOffset RecordedDate { get; set; }

    [Required]
    public int StateId { get; set; }

    [Required]
    [ForeignKey(nameof(Actor))]
    public int ActorId { get; set; }

    [MaxLength(255)]
    public string RejectionReason { get; set; }

    public Expense Expense { get; set; }

    public Employee Actor { get; set; }
}
