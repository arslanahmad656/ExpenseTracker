import React from 'react';
import { FormHistoryTimeline } from './FormHistoryTimeline';

const FormHistoryModal = ({ formId, isOpen, onClose }) => {
    if (!isOpen) return null;

    return (
        <div className="modal show d-block" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
            <div className="modal-dialog modal-xl modal-dialog-centered">
                <div className="modal-content p-4">
                    <FormHistoryTimeline formId={formId} onClose={onClose} />
                </div>
            </div>
        </div>
    );
};

export default FormHistoryModal;
