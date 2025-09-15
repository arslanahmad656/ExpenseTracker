import React, { useState } from 'react';
import CurrencySelect from './CurrencySelect';
import ExpenseItemForm from './ExpenseItemForm';
import { useLocation } from 'react-router-dom';
import { getCallback } from '../../utils/callbackRegistry';

export default function NewExpenseForm({ onSubmit }) {
    const { state } = useLocation();
    const registeredOnSubmitCallback = getCallback(state?.onSubmitCallbackId);
    onSubmit = onSubmit ?? registeredOnSubmitCallback;
	const [title, setTitle] = useState('');
	const [currency, setCurrency] = useState('');
	const today = new Date().toISOString().slice(0, 10);
	const [expenses, setExpenses] = useState([
		{ description: '', amount: '', date: today }
	]);
	const [errors, setErrors] = useState({});
	const [submitError, setSubmitError] = useState('');

	const getRowErrors = (it) => {
		const rowErrors = {};
		if (!it.description.trim()) rowErrors.description = 'Required';
		if (!it.amount || isNaN(Number(it.amount))) rowErrors.amount = 'Valid amount required';
		if (!it.date) rowErrors.date = 'Required';
		return rowErrors;
	};

	const isExpenseValid = (it) => it.description.trim() && it.amount && !isNaN(Number(it.amount)) && it.date;

	const addExpense = () => {
		// prevent adding if any current expense is invalid
		if (expenses.some((e) => !isExpenseValid(e))) {
			setErrors((prev) => ({ ...prev, expenses: 'Complete existing expenses before adding a new one' }));
			return;
		}
		setErrors((prev) => ({ ...prev, expenses: '' }));
		setExpenses(prev => [...prev, { description: '', amount: '', date: today }]);
	};

	const removeExpense = (index) => {
		setExpenses(prev => prev.filter((_, i) => i !== index));
	};

	const updateExpense = (index, updated) => {
		setExpenses(prev => prev.map((it, i) => (i === index ? updated : it)));
		const rowErrors = getRowErrors(updated);
		setErrors(prev => {
			const next = { ...prev };
			if (Object.keys(rowErrors).length === 0) {
				delete next[`expense_${index}`];
			} else {
				next[`expense_${index}`] = rowErrors;
			}
			if (next.expenses && expenses.length > 0) delete next.expenses;
			return next;
		});
	};

	const validate = () => {
		const newErrors = {};
		if (!title.trim()) newErrors.title = 'Title is required';
		if (!currency) newErrors.currency = 'Currency is required';
		if (expenses.length === 0) newErrors.expenses = 'Add at least one expense';
		expenses.forEach((it, i) => {
			const rowErrors = {};
			if (!it.description.trim()) rowErrors.description = 'Required';
			if (!it.amount || isNaN(Number(it.amount))) rowErrors.amount = 'Valid amount required';
			if (!it.date) rowErrors.date = 'Required';
			if (Object.keys(rowErrors).length) newErrors[`expense_${i}`] = rowErrors;
		});
		setErrors(newErrors);
		return Object.keys(newErrors).length === 0;
	};

	const handleSubmit = (e) => {
        debugger;
		e.preventDefault();
		setSubmitError('');
		if (!validate()) return;
		try {
			const payload = { title, currency, expenses: expenses.map(e => ({ ...e, amount: Number(e.amount) })) };
			onSubmit && onSubmit(payload);
		} catch (err) {
			setSubmitError(err.message || 'Failed to submit form');
		}
	};

	return (
		<div className="card shadow-sm border-0">
			<div className="card-body p-4">
				<h5 className="card-title mb-3">Submit a new Expense Detail Form</h5>

				{submitError && (
					<div className="alert alert-danger" role="alert">{submitError}</div>
				)}

				<form onSubmit={handleSubmit} noValidate>
					<div className="row g-3 mb-3">
						<div className="col-md-8">
							<label className="form-label fw-semibold" htmlFor="title">Title</label>
							<input
								id="title"
								type="text"
								className={`form-control ${errors.title ? 'is-invalid' : ''}`}
								value={title}
								onChange={(e) => {
									const v = e.target.value;
									setTitle(v);
									if (errors.title && v.trim()) {
										setErrors(prev => { const n = { ...prev }; delete n.title; return n; });
									}
								}}
								placeholder="e.g., Team Outing"
							/>
							{errors.title && <div className="invalid-feedback">{errors.title}</div>}
						</div>
						<div className="col-md-4">
							<CurrencySelect
								value={currency}
								onChange={(val) => {
									setCurrency(val);
									if (errors.currency && val) {
										setErrors(prev => { const n = { ...prev }; delete n.currency; return n; });
									}
								}}
							/>
							{errors.currency && <div className="text-danger small mt-1">{errors.currency}</div>}
						</div>
					</div>

					<div className="d-flex justify-content-between align-items-center mb-2">
						<h6 className="mb-0">Expenses</h6>
						<button type="button" className="btn btn-outline-primary btn-sm" onClick={addExpense}>
							<i className="bi bi-plus-lg me-1"></i> Add Expense
						</button>
					</div>
					{errors.expenses && <div className="text-danger small mb-2">{errors.expenses}</div>}

					{expenses.map((item, index) => (
						<ExpenseItemForm
							key={index}
							index={index}
							item={item}
							onChange={updateExpense}
							onDelete={removeExpense}
							errors={errors[`expense_${index}`] || {}}
						/>
					))}

					<div className="d-flex justify-content-between align-items-center mt-3">
						<div className="d-flex align-items-center">
							<span className="text-muted me-2 fw-semibold">Total</span>
							<span className="px-3 py-2 rounded-pill bg-light border fw-bold fs-5">
								{currency || 'CUR'} {expenses.reduce((sum, e) => sum + (parseFloat(e.amount) || 0), 0).toFixed(2)}
							</span>
						</div>
						<button type="submit" className="btn btn-primary" disabled={expenses.length === 0}>
							Submit
						</button>
					</div>
				</form>
			</div>
		</div>
	);
}
