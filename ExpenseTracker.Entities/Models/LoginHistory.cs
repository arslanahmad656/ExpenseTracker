using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Contracts;

namespace ExpenseTracker.Entities.Models;

public class LoginHistory : IEntity
{
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(UserRole))]
    public int UserRoleId { get; set; }

    public DateTimeOffset? LoginTime { get; set; }

    public DateTimeOffset? LogoutTime { get; set; }

    [MaxLength(31)]
    public string IPAddress { get; set; }

    public UserRole UserRole { get; set; }
}
