import React from 'react';

export const ApproveFormModal = ({
    isOpen,
    onConfirm,
    onCancel
}) => {
    if (!isOpen) return null;

    return (
        <div className="modal show d-block" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
            <div className="modal-dialog modal-dialog-centered">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">Approve Form</h5>
                        <button 
                            type="button" 
                            className="btn-close" 
                            onClick={onCancel}
                        ></button>
                    </div>
                    <div className="modal-body">
                        <p>Are you sure you want to approve this expense form? This will move all expenses to pending reimbursement status.</p>
                    </div>
                    <div className="modal-footer">
                        <button 
                            type="button" 
                            className="btn btn-secondary" 
                            onClick={onCancel}
                        >
                            Cancel
                        </button>
                        <button 
                            type="button" 
                            className="btn btn-success" 
                            onClick={onConfirm}
                        >
                            <i className="bi bi-check-circle me-1"></i> Approve Form
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};
