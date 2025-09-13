using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Entities.Models;

[Index(nameof(TrackingId), IsUnique = true)]
public class Expense : IEntity
{
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Form))]
    public int FormId { get; set; }

    [Required]
    [MaxLength(15)]
    public string TrackingId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Details { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTimeOffset Date { get; set; }

    [Required]
    [ForeignKey(nameof(ExpenseState))]
    public int ExpenseStateId { get; set; }

    public Form Form { get; set; }

    public ExpenseState ExpenseState { get; set; }

    public ICollection<ExpenseHistory> ExpenseHistories { get; set; }
}
