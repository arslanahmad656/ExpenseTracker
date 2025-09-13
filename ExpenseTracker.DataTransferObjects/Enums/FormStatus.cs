namespace ExpenseTracker.Shared.Enums;

/// <summary>
/// Represents the workflow state of a form.
/// </summary>
public enum FormStatus
{
    /// <summary>
    /// State when an employee has submitted an expense and is now subject to manager's review.
    /// </summary>
    PendingApproval = 1,

    /// <summary>
    /// State when the manager has approved an expense and is now suject to an accountant's approval.
    /// </summary>
    PendingReimbursement,

    /// <summary>
    /// State when the manager has rejected the expense.
    /// </summary>
    Rejected,

    /// <summary>
    /// State when the manager has reimbursed the amount.
    /// </summary>
    Reimbursed,

    /// <summary>
    /// State when the submitter of the expense deliberately cancelled the expense.
    /// </summary>
    Cancelled
}