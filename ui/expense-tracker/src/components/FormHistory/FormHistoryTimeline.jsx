import React, { useState, useEffect } from 'react';
import { httpClient } from '../../api/httpClient';
import { endPoints } from '../../utils/endPoints';
import './FormHistoryTimeline.css';

const FormHistoryTimeline = ({ formId, onClose }) => {
    const [historyRecords, setHistoryRecords] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState('');

    useEffect(() => {
        const fetchHistory = async () => {
            if (!formId) return;
            
            setLoading(true);
            setError('');
            
            try {
                const response = await httpClient.get(endPoints.formHistoryRecords(formId));
                setHistoryRecords(response.data || []);
            } catch (err) {
                setError(err.message || 'Failed to fetch form history');
                setHistoryRecords([]);
            } finally {
                setLoading(false);
            }
        };

        fetchHistory();
    }, [formId]);

    if (loading) {
        return (
            <div className="d-flex justify-content-center align-items-center" style={{ minHeight: '200px' }}>
                <div className="text-center">
                    <div className="spinner-border text-primary mb-2" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </div>
                    <div className="text-muted">Loading form history...</div>
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

    if (historyRecords.length === 0) {
        return (
            <div className="text-center text-muted py-4">
                <i className="bi bi-clock-history fs-1 mb-3 d-block"></i>
                <h5>No History Available</h5>
                <p>No history records found for this form.</p>
            </div>
        );
    }

    return (
        <div className="form-history-timeline">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h4 className="mb-0">
                    <i className="bi bi-clock-history me-2"></i>
                    Form History
                </h4>
                <button 
                    type="button" 
                    className="btn-close" 
                    onClick={onClose}
                    aria-label="Close"
                ></button>
            </div>

            <div className="timeline-container">
                {historyRecords.map((record, index) => (
                    <div key={index} className="timeline-item">
                        <div className="timeline-marker">
                            <div className="timeline-dot">
                                {index === 0 && <i className="bi bi-play-fill"></i>}
                                {index === historyRecords.length - 1 && <i className="bi bi-check-circle-fill"></i>}
                                {index > 0 && index < historyRecords.length - 1 && <i className="bi bi-circle-fill"></i>}
                            </div>
                            {index < historyRecords.length - 1 && <div className="timeline-line"></div>}
                        </div>
                        <div className="timeline-content">
                            <div className="timeline-record">
                                {record}
                            </div>
                        </div>
                    </div>
                ))}
            </div>

            <div className="mt-4 pt-3 border-top">
                <div className="row text-muted small">
                    <div className="col-6">
                        <i className="bi bi-info-circle me-1"></i>
                        {historyRecords.length} history record{historyRecords.length !== 1 ? 's' : ''}
                    </div>
                    <div className="col-6 text-end">
                        <i className="bi bi-clock me-1"></i>
                        Form ID: {formId}
                    </div>
                </div>
            </div>
        </div>
    );
};

export { FormHistoryTimeline };
export default FormHistoryTimeline;