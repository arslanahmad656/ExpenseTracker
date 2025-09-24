import React from 'react';

export const CancelFormModal = ({
    isOpen,
    formCancellationReason,
    onReasonChange,
    onConfirm,
    onCancel
}) => {
    if (!isOpen) return null;

    return (
        <div className="modal show d-block" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
            <div className="modal-dialog modal-dialog-centered">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">Cancel Form</h5>
                        <button 
                            type="button" 
                            className="btn-close" 
                            onClick={onCancel}
                        ></button>
                    </div>
                    <div className="modal-body">
                        <p>Are you sure you want to cancel this expense form? This action cannot be undone.</p>
                        <p className="text-muted small">All expenses will be marked as cancelled and the form will be closed.</p>
                        <div className="mb-3">
                            <label htmlFor="formCancellationReason" className="form-label">
                                Cancellation Reason <span className="text-danger">*</span>
                            </label>
                            <textarea
                                id="formCancellationReason"
                                className="form-control"
                                rows="3"
                                value={formCancellationReason}
                                onChange={(e) => onReasonChange(e.target.value)}
                                placeholder="Please provide a reason for cancelling this form..."
                                required
                            />
                            {!formCancellationReason.trim() && (
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
                            Keep Form
                        </button>
                        <button 
                            type="button" 
                            className="btn btn-warning" 
                            onClick={onConfirm}
                            disabled={!formCancellationReason.trim()}
                        >
                            <i className="bi bi-x-circle me-1"></i> Cancel Form
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};
