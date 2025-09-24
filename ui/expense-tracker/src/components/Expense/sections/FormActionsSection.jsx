import React from 'react';

export const FormActionsSection = ({
    mode,
    canUpdateForm,
    totalAmount,
    currencyCode,
    expensesLength,
    onSubmit,
    onCancelForm,
    effectiveFormId
}) => {
    return (
        <div className="d-flex justify-content-between align-items-center mt-3">
            <div className="d-flex flex-column">
                <div className="d-flex align-items-center mb-1">
                    <span className="text-muted me-2 fw-semibold">Total</span>
                    <span className="px-3 py-2 rounded-pill bg-light border fw-bold fs-5">
                        {currencyCode} {totalAmount}
                    </span>
                </div>
                <small className="text-muted">
                    <i className="bi bi-info-circle me-1"></i>
                    Excludes cancelled expenses.
                </small>
            </div>
            {canUpdateForm && mode !== 'manager' && mode !== 'accountant' && (
                <div className="d-flex gap-2">
                    {mode === 'update' && effectiveFormId && (
                        <button 
                            type="button" 
                            className="btn btn-outline-warning" 
                            onClick={onCancelForm}
                        >
                            <i className="bi bi-x-circle me-1"></i> Cancel Form
                        </button>
                    )}
                    <button type="submit" className="btn btn-primary" disabled={expensesLength === 0}>
                        Submit
                    </button>
                </div>
            )}
        </div>
    );
};
