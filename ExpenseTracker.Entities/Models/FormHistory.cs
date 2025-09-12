using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseTracker.Contracts;

namespace ExpenseTracker.Entities.Models;

public class FormHistory : IEntity
{
    public int Id { get; set; }

    [Required]
    [ForeignKey(nameof(Form))]
    public int FormId { get; set; }

    [Required]
    public int StateId { get; set; }

    [Required]
    public DateTimeOffset RecordedDate { get; set; }

    [Required]
    [ForeignKey(nameof(Actor))]
    public int ActorId { get; set; }

    public Form Form { get; set; }

    public Employee Actor { get; set; }
}
