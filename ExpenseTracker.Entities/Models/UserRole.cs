using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Entities.Models;

[Index(nameof(PrincipalId), nameof(RoleId), IsUnique = true)]
public class UserRole : IEntity
{
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Principal))]
    public int PrincipalId { get; set; }

    [Required]
    [ForeignKey(nameof(Role))]
    public int RoleId { get; set; }

    public Principal Principal { get; set; }

    public Role Role { get; set; }

    public ICollection<LoginHistory> LoginHistories { get; set; }
}
