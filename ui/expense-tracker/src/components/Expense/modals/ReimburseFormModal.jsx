import React from 'react';

export const ReimburseFormModal = ({
    isOpen,
    currencyCode,
    totalAmount,
    onConfirm,
    onCancel
}) => {
    if (!isOpen) return null;

    return (
        <div className="modal show d-block" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
            <div className="modal-dialog modal-dialog-centered">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">Reimburse Form</h5>
                        <button 
                            type="button" 
                            className="btn-close" 
                            onClick={onCancel}
                        ></button>
                    </div>
                    <div className="modal-body">
                        <p>
                            This action will reimburse all expenses in this form (total: <strong>{currencyCode} {totalAmount}</strong>) and add the amount to the employee's account. This action cannot be undone. Do you want to proceed?
                        </p>
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
                            <i className="bi bi-check-circle me-1"></i> Reimburse Form
                        </button>
                    </div>
                </div>
            </div>
        </div>
    );
};
