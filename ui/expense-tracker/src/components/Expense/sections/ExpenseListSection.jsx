import React from 'react';
import ExpenseItemForm from '../ExpenseItemForm';
import { ExpenseStatus } from '../../../utils/enums';

export const ExpenseListSection = ({
    expenses,
    errors,
    onAddExpense,
    onUpdateExpense,
    onDeleteExpense,
    onCancelExpense,
    onApproveForm,
    onRejectForm,
    onReimburseForm,
    mode,
    canUpdateForm,
    isFormReadOnly,
    isFormApproved
}) => {
    // Check if expense is locked (cannot be edited)
    const isExpenseLocked = (expenseStatus) => 
        expenseStatus === ExpenseStatus.PendingReimbursement || 
        expenseStatus === ExpenseStatus.Reimbursed || 
        expenseStatus === ExpenseStatus.Cancelled;

    return (
        <>
            <div className="d-flex justify-content-between align-items-center mb-2">
                <h6 className="mb-0">Expenses</h6>
                <div>
                    {canUpdateForm && (
                        <>
                            {mode === 'manager' ? (
                                <>
                                    <button 
                                        type="button" 
                                        className="btn btn-success btn-sm me-2" 
                                        onClick={onApproveForm}
                                        disabled={isFormApproved}
                                    >
                                        <i className="bi bi-check-circle me-1"></i> Approve Form
                                    </button>
                                    <button 
                                        type="button" 
                                        className="btn btn-danger btn-sm" 
                                        onClick={onRejectForm}
                                        disabled={isFormApproved}
                                    >
                                        <i className="bi bi-x-circle me-1"></i> Reject Form
                                    </button>
                                </>
                            ) : mode === 'accountant' ? (
                                <>
                                    <button 
                                        type="button" 
                                        className="btn btn-success btn-sm" 
                                        onClick={onReimburseForm}
                                        disabled={isFormApproved}
                                    >
                                        <i className="bi bi-check-circle me-1"></i> Reimburse Form
                                    </button>
                                </>
                            ) : (
                                <>
                                    <button 
                                        type="button" 
                                        className="btn btn-outline-primary btn-sm me-2" 
                                        onClick={onAddExpense}
                                    >
                                        <i className="bi bi-plus-lg me-1"></i> Add Expense
                                    </button>
                                </>
                            )}
                        </>
                    )}
                </div>
            </div>
            {errors.expenses && <div className="text-danger small mb-2">{errors.expenses}</div>}

            {expenses.map((item, index) => {
                const isNewExpense = !item.id; // New expenses don't have an id
                const canUpdateThisExpense = true;
                const isLocked = isExpenseLocked(item.status);
                
                // Get lock color and tooltip based on status
                const getLockInfo = (status) => {
                    switch (status) {
                        case ExpenseStatus.PendingReimbursement:
                            return { color: 'text-info', tooltip: 'Locked - Pending reimbursement approval' };
                        case ExpenseStatus.Reimbursed:
                            return { color: 'text-success', tooltip: 'Locked - Already reimbursed' };
                        case ExpenseStatus.Cancelled:
                            return { color: 'text-secondary', tooltip: 'Locked - Expense cancelled' };
                        default:
                            return { color: 'text-muted', tooltip: 'This expense cannot be edited' };
                    }
                };
                
                const lockInfo = isLocked ? getLockInfo(item.status) : null;
                
                return (
                    <div key={index} className="position-relative">
                        {isLocked && lockInfo && (
                            <div className="position-absolute top-0 end-0 p-2" style={{ zIndex: 10 }}>
                                <i className={`bi bi-lock-fill ${lockInfo.color}`} title={lockInfo.tooltip}></i>
                            </div>
                        )}
                        <ExpenseItemForm
                            index={index}
                            item={item}
                            onChange={onUpdateExpense}
                            onDelete={isNewExpense ? onDeleteExpense : (() => onCancelExpense(item))}
                            onApprove={null}
                            errors={errors[`expense_${index}`] || {}}
                            readOnly={isFormReadOnly || (!isNewExpense && !canUpdateThisExpense) || isLocked}
                            showCancelButton={!isNewExpense && mode === 'update' && canUpdateForm && canUpdateThisExpense && !isLocked}
                            showRejectButton={false}
                            showRejectionReason={item.status === ExpenseStatus.Rejected && item.rejectionReason}
                            isFormApproved={isFormApproved}
                            mode={mode}
                        />
                    </div>
                );
            })}
        </>
    );
};
