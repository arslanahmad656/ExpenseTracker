import React from 'react';
import CurrencySelect from '../CurrencySelect';

export const ExpenseFormHeader = ({
    titleValue,
    currency,
    errors,
    onTitleChange,
    onCurrencyChange,
    isReadOnly
}) => {
    return (
        <div className="row g-3 mb-3">
            <div className="col-md-8">
                <label className="form-label fw-semibold" htmlFor="title">Title</label>
                <input
                    id="title"
                    type="text"
                    className={`form-control ${errors.title ? 'is-invalid' : ''}`}
                    value={titleValue}
                    onChange={(e) => {
                        const v = e.target.value;
                        onTitleChange(v);
                        if (errors.title && v.trim()) {
                            onTitleChange(v, true); // Pass true to indicate clearing error
                        }
                    }}
                    placeholder="e.g., Team Outing"
                    readOnly={isReadOnly}
                    disabled={isReadOnly}
                />
                {errors.title && <div className="invalid-feedback">{errors.title}</div>}
            </div>
            <div className="col-md-4">
                <CurrencySelect
                    value={currency}
                    onChange={(val) => {
                        onCurrencyChange(val);
                        if (errors.currency && val) {
                            onCurrencyChange(val, true); // Pass true to indicate clearing error
                        }
                    }}
                    readOnly={isReadOnly}
                    disabled={isReadOnly}
                />
                {errors.currency && <div className="text-danger small mt-1">{errors.currency}</div>}
            </div>
        </div>
    );
};
