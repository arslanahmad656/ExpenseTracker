import React from 'react';
import { FormStatus } from '../../../utils/enums';

export const FormStatusAlerts = ({
    formStatus,
    formData,
    mode,
    isFormApproved
}) => {
    return (
        <>
            {/* Form-level rejection reason */}
            {formStatus === FormStatus.Rejected && formData?.rejectionReason && (
                <div className="row mb-3">
                    <div className="col-12">
                        <div className="alert alert-warning d-flex align-items-start" role="alert">
                            <i className="bi bi-exclamation-triangle-fill me-2 mt-1"></i>
                            <div>
                                <strong>Form Rejection Reason:</strong>
                                <p className="mb-0 mt-1">{formData.rejectionReason}</p>
                            </div>
                        </div>
                    </div>
                </div>
            )}

            {/* Form approval indication */}
            {mode === 'manager' && isFormApproved && (
                <div className="row mb-3">
                    <div className="col-12">
                        <div className="alert alert-success d-flex align-items-start" role="alert">
                            <i className="bi bi-check-circle-fill me-2 mt-1"></i>
                            <div>
                                <strong>Form Already Approved</strong>
                                <p className="mb-0 mt-1">
                                    This form has already been approved and is now in the reimbursement process. 
                                    No further actions can be taken on this form.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </>
    );
};
