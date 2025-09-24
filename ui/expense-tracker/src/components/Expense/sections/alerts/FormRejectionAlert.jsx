import React from 'react';

export const FormRejectionAlert = ({ rejectionReason }) => {
    if (!rejectionReason) return null;

    return (
        <div className="row mb-3">
            <div className="col-12">
                <div className="alert alert-warning d-flex align-items-start" role="alert">
                    <i className="bi bi-exclamation-triangle-fill me-2 mt-1"></i>
                    <div>
                        <strong>Form Rejection Reason:</strong>
                        <p className="mb-0 mt-1">{rejectionReason}</p>
                    </div>
                </div>
            </div>
        </div>
    );
};
