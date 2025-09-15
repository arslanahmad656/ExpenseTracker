import React from 'react';

export default function ExpenseItemForm({ index, item, onChange, onDelete, errors = {} }) {
	const handleChange = (e) => {
		const { name, value } = e.target;
		onChange(index, { ...item, [name]: name === 'amount' ? value.replace(/[^0-9.]/g, '') : value });
	};

	return (
		<div className={`border rounded p-3 mb-3 bg-light ${Object.keys(errors).length ? 'border-danger' : ''}`}>
			<div className="row g-3">
				<div className="col-md-6">
					<label className="form-label fw-semibold" htmlFor={`desc-${index}`}>Description</label>
					<input
						type="text"
						className={`form-control ${errors.description ? 'is-invalid' : ''}`}
						id={`desc-${index}`}
						name="description"
						value={item.description}
						onChange={handleChange}
						placeholder="e.g., Lunch with client"
					/>
					{errors.description ? (
						<div className="invalid-feedback d-block">{errors.description}</div>
					) : (
						<div className="invalid-feedback d-block" style={{ visibility: 'hidden' }}>placeholder</div>
					)}
				</div>
				<div className="col-md-3">
					<label className="form-label fw-semibold" htmlFor={`amount-${index}`}>Amount</label>
					<input
						type="number"
						className={`form-control ${errors.amount ? 'is-invalid' : ''}`}
						id={`amount-${index}`}
						name="amount"
						min="0"
						step="0.01"
						value={item.amount}
						onChange={handleChange}
						placeholder="0.00"
					/>
					{errors.amount ? (
						<div className="invalid-feedback d-block">{errors.amount}</div>
					) : (
						<div className="invalid-feedback d-block" style={{ visibility: 'hidden' }}>placeholder</div>
					)}
				</div>
				<div className="col-md-3">
					<label className="form-label fw-semibold" htmlFor={`date-${index}`}>Date</label>
					<input
						type="date"
						className={`form-control ${errors.date ? 'is-invalid' : ''}`}
						id={`date-${index}`}
						name="date"
						value={item.date}
						onChange={handleChange}
					/>
					{errors.date ? (
						<div className="invalid-feedback d-block">{errors.date}</div>
					) : (
						<div className="invalid-feedback d-block" style={{ visibility: 'hidden' }}>placeholder</div>
					)}
				</div>
				<div className="col-12 d-flex justify-content-end">
					<button type="button" className="btn btn-outline-danger btn-sm" onClick={() => onDelete(index)}>
						<i className="bi bi-trash me-1"></i> Remove
					</button>
				</div>
			</div>
		</div>
	);
}
