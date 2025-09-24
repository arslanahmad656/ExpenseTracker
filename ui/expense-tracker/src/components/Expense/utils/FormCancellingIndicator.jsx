import React from 'react';

export const FormCancellingIndicator = () => {
    return (
        <div className="card shadow-sm border-0">
            <div className="card-body p-4">
                <div className="text-center py-5">
                    <div className="spinner-border text-warning mb-3" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </div>
                    <h5 className="text-warning">Cancelling form...</h5>
                    <p className="text-muted">Please wait while we process your request.</p>
                </div>
            </div>
        </div>
    );
};
