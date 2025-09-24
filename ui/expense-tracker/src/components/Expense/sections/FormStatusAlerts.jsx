import React from 'react';
import { FormStatus } from '../../../utils/enums';
import { FormRejectionAlert, FormApprovalAlert } from './alerts';

export const FormStatusAlerts = ({
    formStatus,
    formData,
    mode,
    isFormApproved
}) => {
    return (
        <>
            {/* Form-level rejection reason */}
            {formStatus === FormStatus.Rejected && (
                <FormRejectionAlert rejectionReason={formData?.rejectionReason} />
            )}

            {/* Form approval indication for manager mode */}
            {mode === 'manager' && isFormApproved && (
                <FormApprovalAlert />
            )}
        </>
    );
};
