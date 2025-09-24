import React from 'react';

export const GenericListLoadingStates = ({
    structureLoading,
    loading,
    data,
    error
}) => {
    if (structureLoading) {
        return (
            <div className="d-flex justify-content-center align-items-center" style={{ minHeight: '200px' }}>
                <div className="text-center">
                    <div className="spinner-border text-primary mb-2" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </div>
                    <div className="text-muted">Loading grid structure...</div>
                </div>
            </div>
        );
    }

    if (loading && data.length === 0) {
        return (
            <div className="d-flex justify-content-center align-items-center" style={{ minHeight: '200px' }}>
                <div className="text-center">
                    <div className="spinner-border text-primary mb-2" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </div>
                    <div className="text-muted">Loading data...</div>
                </div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="alert alert-danger" role="alert">
                <i className="bi bi-exclamation-triangle me-2"></i>
                {error}
            </div>
        );
    }

    return null;
};
