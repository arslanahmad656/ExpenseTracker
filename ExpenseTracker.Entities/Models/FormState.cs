using System.ComponentModel.DataAnnotations;
using ExpenseTracker.Shared.Contracts;
using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Entities.Models;

public class FormState : IEntity
{
    public int Id { get; set; }

    [Required]
    public FormStatus State { get; set; }

    public ICollection<Form> Forms { get; set; }
}
