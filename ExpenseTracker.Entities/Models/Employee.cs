using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Contracts;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Entities.Models;

[Index(nameof(Code), IsUnique = true)]
public class Employee : IEntity
{
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Principal))]
    public int PrincipalId { get; set; }

    [Required]
    [MaxLength(15)]
    public string Code { get; set; }

    [Required]
    [MaxLength(127)]
    public string Name { get; set; }

    [ForeignKey(nameof(Manager))]
    public int? ManagerId { get; set; }

    [Required]
    public DateTimeOffset HireDate { get; set; }

    public Principal Principal { get; set; }

    public Employee Manager { get; set; }

    public ICollection<FormHistory> FormHistories { get; set; }

    public ICollection<ExpenseHistory> ExpenseHistories { get; set; }

    [InverseProperty(nameof(Manager))]
    public ICollection<Employee> DirectReports { get; set; }
}
