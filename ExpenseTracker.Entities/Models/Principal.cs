using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Entities.Models;

[Index(nameof(Username), IsUnique = true)]
public class Principal : IEntity
{
    public int Id { get; set; }

    [Required]
    [MaxLength(31)]
    public string Username { get; set; }

    [Required]
    [MaxLength(127)]
    public string PasswordHash { get; set; }

    [Required]
    public bool IsActive { get; set; }

    public Employee Employee { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}
