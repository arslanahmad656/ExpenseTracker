import React from 'react';

export const RejectFormModal = ({
    isOpen,
    formRejectionReason,
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
                        <h5 className="modal-title">Reject Form</h5>
                        <button 
                            type="button" 
                            className="btn-close" 
                            onClick={onCancel}
                        ></button>
                    </div>
                    <div className="modal-body">
                        <p>Are you sure you want to reject this expense form? This action cannot be undone.</p>
                        <div className="mb-3">
                            <label htmlFor="formRejectionReason" className="form-label">
                                Rejection Reason <span className="text-danger">*</span>
                            </label>
                            <textarea
                                id="formRejectionReason"
                                className="form-control"
                                rows="3"
                                value={formRejectionReason}
                                onChange={(e) => onReasonChange(e.target.value)}
                                placeholder="Please provide a reason for rejecting this form..."
                                required
                            />
                            {!formRejectionReason.trim() && (
                                <div className="invalid-feedback d-block">
                                    Rejection reason is required.
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
                            className="btn btn-danger" 
                            onClick={onConfirm}
                            disabled={!formRejectionReason.trim()}
                        >
                            <i className="bi bi-x-circle me-1"></i> Reject Form
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};
