import React from 'react';

const DEFAULT_CURRENCIES = [
	{ code: 'USD', label: 'US Dollar (USD)' },
	{ code: 'EUR', label: 'Euro (EUR)' },
	{ code: 'GBP', label: 'British Pound (GBP)' },
	{ code: 'INR', label: 'Indian Rupee (INR)' },
	{ code: 'JPY', label: 'Japanese Yen (JPY)' },
	{ code: 'AUD', label: 'Australian Dollar (AUD)' },
	{ code: 'CAD', label: 'Canadian Dollar (CAD)' },
];

export default function CurrencySelect({ value, onChange, currencies = DEFAULT_CURRENCIES, id = 'currency', required = true, readOnly = false, disabled = false }) {
	return (
		<div>
			<label className="form-label fw-semibold" htmlFor={id}>Currency</label>
			<select 
				id={id} 
				className="form-select" 
				value={value} 
				onChange={(e) => onChange(e.target.value)} 
				required={required}
				readOnly={readOnly}
				disabled={disabled}
			>
				<option value="">Select currency</option>
				{currencies.map((c) => (
					<option key={c.code} value={c.code}>{c.label}</option>
				))}
			</select>
			<div className="form-text">All expenses in this form will use this currency.</div>
		</div>
	);
}
