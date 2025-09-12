using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Entities.Models;

[Index(nameof(Name), IsUnique = true)]
public class Role : IEntity
{
    public int Id { get; set; }

    [Required]
    [MaxLength(31)]
    public string Name { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}
