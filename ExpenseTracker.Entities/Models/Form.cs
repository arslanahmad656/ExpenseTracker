using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Shared.Contracts;
using ExpenseTracker.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Entities.Models;

[Index(nameof(TrackingId), IsUnique = true)]
public class Form : IEntity
{
    public int Id { get; set; }

    [Required]
    [MaxLength(32)]
    public string TrackingId { get; set; }

    [Required]
    [MaxLength(31)]
    public string Title { get; set; }

    [Required]
    [ForeignKey(nameof(Currency))]
    public int CurrencyId { get; set; }

    [Required]
    public FormStatus Status { get; set; }

    public Currency Currency { get; set; }

    public ICollection<FormHistory> FormHistories { get; set; }

    public ICollection<Expense> Expenses { get; set; }
}
