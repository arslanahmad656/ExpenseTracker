using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Shared.Contracts;
using ExpenseTracker.Shared.Enums;
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
    [MaxLength(32)]
    public string TrackingId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Details { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTimeOffset Date { get; set; }

    [Required]
    public ExpenseStatus Status { get; set; }

    public Form Form { get; set; }

    public ICollection<ExpenseHistory> ExpenseHistories { get; set; }
}
