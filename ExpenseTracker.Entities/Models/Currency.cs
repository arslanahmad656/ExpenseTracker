using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Entities.Models;

[Index(nameof(Code), IsUnique = false, Name = "IX_Currency_Code")]
public class Currency : IEntity
{
    public int Id { get; set; }

    [Required]
    [MaxLength(15)]
    public string Code { get; set; }

    [Required]
    [MaxLength(5)]
    public string Symbol { get; set; }

    [Required]
    [MaxLength(31)]
    public string FullName { get; set; }

    public ICollection<Form> Forms { get; set; }
}
