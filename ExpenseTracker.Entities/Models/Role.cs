using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Entities.Models;

[Index(nameof(Name), IsUnique = true)]
public class Role
{
    public int Id { get; set; }

    [Required]
    [MaxLength(31)]
    public string Name { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}
