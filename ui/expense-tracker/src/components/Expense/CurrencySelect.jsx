import React, { useState, useEffect } from 'react';
import { httpClient } from '../../api/httpClient';
import { endPoints } from '../../utils/endPoints';

const DEFAULT_CURRENCIES = [
	{ code: 'USD', label: 'US Dollar (USD)' },
	{ code: 'EUR', label: 'Euro (EUR)' },
	{ code: 'GBP', label: 'British Pound (GBP)' },
	{ code: 'INR', label: 'Indian Rupee (INR)' },
	{ code: 'JPY', label: 'Japanese Yen (JPY)' },
	{ code: 'AUD', label: 'Australian Dollar (AUD)' },
	{ code: 'CAD', label: 'Canadian Dollar (CAD)' },
];

export default function CurrencySelect({ value, onChange, currencies, id = 'currency', required = true, readOnly = false, disabled = false }) {
	const [currenciesList, setCurrenciesList] = useState(DEFAULT_CURRENCIES);
	const [loading, setLoading] = useState(false);
	const [error, setError] = useState('');

	// Fetch currencies from server
	useEffect(() => {
		const fetchCurrencies = async () => {
			// If currencies are passed as prop, use them instead of fetching
			if (currencies) {
				setCurrenciesList(currencies);
				return;
			}

			setLoading(true);
			setError('');
			
			try {
				const response = await httpClient.get(endPoints.currenciesForDropdown());
				setCurrenciesList(response.data || DEFAULT_CURRENCIES);
			} catch (err) {
				console.error('Failed to fetch currencies:', err);
				setError('Failed to load currencies');
				// Keep default currencies on error
				setCurrenciesList(DEFAULT_CURRENCIES);
			} finally {
				setLoading(false);
			}
		};

		fetchCurrencies();
	}, [currencies]);
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
				disabled={disabled || loading}
			>
				<option value="">
					{loading ? 'Loading currencies...' : 'Select currency'}
				</option>
				{currenciesList.map((c) => (
					<option key={c.code} value={c.code}>{c.label}</option>
				))}
			</select>
			{error && (
				<div className="text-danger small mt-1">
					<i className="bi bi-exclamation-triangle me-1"></i>
					{error}
				</div>
			)}
			<div className="form-text">All expenses in this form will use this currency.</div>
		</div>
	);
}
