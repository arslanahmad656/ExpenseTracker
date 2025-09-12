using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Entities.Models;

public class FormState
{
    public int Id { get; set; }

    [Required]
    public FormStatus State { get; set; }

    public ICollection<Form> Forms { get; set; }
}
