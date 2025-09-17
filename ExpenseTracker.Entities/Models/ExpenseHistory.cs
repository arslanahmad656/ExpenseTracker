using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Shared.Contracts;
using ExpenseTracker.Shared.Enums;

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
    public FormStatus Status { get; set; }

    [MaxLength(2047)]
    public string PreviousState { get; set; }

    [MaxLength(2047)]
    public string CurrentState { get; set; }

    [Required]
    [ForeignKey(nameof(Actor))]
    public int ActorId { get; set; }

    [MaxLength(255)]
    public string Note { get; set; }

    public Expense Expense { get; set; }

    public Employee Actor { get; set; }
}
