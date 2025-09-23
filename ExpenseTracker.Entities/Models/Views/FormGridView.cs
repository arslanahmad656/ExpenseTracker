using ExpenseTracker.Shared.Contracts;
using ExpenseTracker.Shared.Enums;

namespace ExpenseTracker.Entities.Models.Views;

public class FormGridView : IEntity
{
    public int FormId { get; set; }

    public string TrackingId { get; set; }

    public string Title { get; set; }

    public string CurrencyCode { get; set; }

    public decimal Amount { get; set; }

    //public int SubmitterId { get; set; }

    //public int ManagerId { get; set; }

    public FormStatus Status { get; set; }
}
