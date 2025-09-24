import React from 'react';

export const CancelExpenseModal = ({
    isOpen,
    expenseToCancel,
    expenseCancellationReason,
    onReasonChange,
    onConfirm,
    onCancel
}) => {
    if (!isOpen || !expenseToCancel) return null;

    return (
        <div className="modal show d-block" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
            <div className="modal-dialog modal-dialog-centered">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">Cancel Expense</h5>
                        <button 
                            type="button" 
                            className="btn-close" 
                            onClick={onCancel}
                        ></button>
                    </div>
                    <div className="modal-body">
                        <p>Are you sure you want to cancel this expense? This action cannot be undone.</p>
                        <div className="mb-3">
                            <label htmlFor="cancellationReason" className="form-label">
                                Cancellation Reason <span className="text-danger">*</span>
                            </label>
                            <textarea
                                id="cancellationReason"
                                className="form-control"
                                rows="3"
                                value={expenseCancellationReason}
                                onChange={(e) => onReasonChange(e.target.value)}
                                placeholder="Please provide a reason for cancelling this expense..."
                                required
                            />
                            {!expenseCancellationReason.trim() && (
                                <div className="invalid-feedback d-block">
                                    Cancellation reason is required.
                                </div>
                            )}
                        </div>
                    </div>
                    <div className="modal-footer">
                        <button 
                            type="button" 
                            className="btn btn-secondary" 
                            onClick={onCancel}
                        >
                            Keep Expense
                        </button>
                        <button 
                            type="button" 
                            className="btn btn-danger" 
                            onClick={onConfirm}
                            disabled={!expenseCancellationReason.trim()}
                        >
                            <i className="bi bi-x-lg me-1"></i> Cancel Expense
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};
