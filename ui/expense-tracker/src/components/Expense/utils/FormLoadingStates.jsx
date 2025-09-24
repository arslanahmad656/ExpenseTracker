import React from 'react';
import { FormStatus } from '../../../utils/enums';
import LoadingCard from '../../UtilComps/LoadingCard';
import { FormNonEditableCard } from './FormNonEditableCard';
import { FormCancellingIndicator } from './FormCancellingIndicator';

export const FormLoadingStates = ({
    loading,
    error,
    isCancelling,
    formStatus,
    mode,
    effectiveFormId
}) => {
    if (loading) {
        return (
            <LoadingCard message="Loading..." />
        );
    }

    if (error) {
        return (
            <div className="alert alert-danger" role="alert">{error}</div>
        );
    }

    // Check if form is editable based on status
    const isFormEditable = (formStatus) => {
        return formStatus === FormStatus.PendingApproval || formStatus === FormStatus.Rejected;
    };

    // Show non-editable message for certain form statuses
    if (mode === 'update' && formStatus && !isFormEditable(formStatus)) {
        return (
            <FormNonEditableCard 
                formStatus={formStatus} 
                effectiveFormId={effectiveFormId} 
            />
        );
    }

    // Show loading indicator when cancelling
    if (isCancelling) {
        return <FormCancellingIndicator />;
    }

    return null; // No special state to show
};
