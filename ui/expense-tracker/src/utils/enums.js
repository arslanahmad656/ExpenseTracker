export const ExpenseStatus = Object.freeze({
    PendingApproval: 1,       // State when an employee has submitted an expense and is now subject to manager's review.
    PendingReimbursement: 2,  // State when the manager has approved an expense and is now subject to an accountant's approval.
    Rejected: 3,              // State when the manager has rejected the expense.
    Reimbursed: 4,            // State when the manager has reimbursed the amount.
    Cancelled: 5              // State when the submitter of the expense deliberately cancelled the expense.
  });

export const FormStatus = Object.freeze({
    PendingApproval: 1,       // State when an employee has submitted an expense and is now subject to manager's review.
    PendingReimbursement: 2,  // State when the manager has approved an expense and is now subject to an accountant's approval.
    Rejected: 3,              // State when the manager has rejected the expense.
    Reimbursed: 4,            // State when the manager has reimbursed the amount.
    Cancelled: 5              // State when the submitter of the expense deliberately cancelled the expense.
  });


export function StatusToReadableString(status) {
    switch (status) {
        case ExpenseStatus.PendingApproval: return 'Manager Approval Required';
        case ExpenseStatus.PendingReimbursement: return 'Accountant Approval Required';
        case ExpenseStatus.Rejected: return 'Rejected by Manager';
        case ExpenseStatus.Reimbursed: return 'Reimbursed';
        case ExpenseStatus.Cancelled: return 'Cancelled By User';
        default: return undefined;
    }
}