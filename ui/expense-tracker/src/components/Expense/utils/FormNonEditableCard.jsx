import React from 'react';
import { useNavigate } from 'react-router-dom';
import { FormStatus } from '../../../utils/enums';

export const FormNonEditableCard = ({ formStatus, effectiveFormId }) => {
    const navigate = useNavigate();

    const getNonEditableMessage = (status) => {
        switch (status) {
            case FormStatus.Cancelled:
                return {
                    title: 'Form Cancelled',
                    message: 'This expense form has been cancelled and is no longer editable.',
                    icon: 'bi-x-circle',
                    color: 'text-secondary'
                };
            case FormStatus.Reimbursed:
                return {
                    title: 'Form Reimbursed',
                    message: 'This expense form has been fully reimbursed and is no longer editable.',
                    icon: 'bi-check-circle',
                    color: 'text-success'
                };
            case FormStatus.PendingReimbursement:
                return {
                    title: 'Form Under Review',
                    message: 'This expense form is pending reimbursement approval and cannot be edited.',
                    icon: 'bi-clock',
                    color: 'text-info'
                };
            default:
                return {
                    title: 'Form Not Editable',
                    message: 'This expense form is not editable in its current state.',
                    icon: 'bi-lock',
                    color: 'text-muted'
                };
        }
    };

    const messageInfo = getNonEditableMessage(formStatus);
    
    return (
        <div className="card shadow-sm border-0">
            <div className="card-body p-5 text-center">
                <i className={`bi ${messageInfo.icon} ${messageInfo.color} mb-3`} style={{ fontSize: '3rem' }}></i>
                <h4 className={`${messageInfo.color} mb-3`}>{messageInfo.title}</h4>
                <p className="text-muted mb-4">{messageInfo.message}</p>
                <button 
                    className="btn btn-outline-primary"
                    onClick={() => navigate(`/form/${effectiveFormId}/details`)}
                >
                    <i className="bi bi-eye me-1"></i>
                    View Details
                </button>
            </div>
        </div>
    );
};
