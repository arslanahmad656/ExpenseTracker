import React, { useEffect, useState } from 'react';
import ExpenseSummaryCard from './ExpenseSummaryCard';
import ExpenseList from './ExpenseList';
import formService from '../../../api/formService';
import { useParams, useNavigate } from 'react-router-dom';
import { ExpenseStatus, FormStatus } from '../../../utils/enums';

export default function ExpenseDetailsView({ formId }) {
	const params = useParams();
	const navigate = useNavigate();
	const effectiveFormId = formId ?? params?.formId ?? params?.id;
	const [loading, setLoading] = useState(true);
	const [error, setError] = useState('');
	const [data, setData] = useState(null);

	useEffect(() => {
		let isMounted = true;
		async function load() {
			setLoading(true);
			setError('');
			try {
				const result = await formService.getDetailedForm(effectiveFormId);
				if (!isMounted) return;
				setData(result);
			} catch (err) {
				if (!isMounted) return;
				setError(err?.message || 'Failed to load form details');
			} finally {
				if (isMounted) setLoading(false);
			}
		}
		if (effectiveFormId != null) load();
		return () => { isMounted = false; };
	}, [effectiveFormId]);

	if (loading) {
		return (
			<div className="card border-0 shadow-sm">
				<div className="card-body">
					Loading...
				</div>
			</div>
		);
	}

	if (error) {
		return (
			<div className="alert alert-danger" role="alert">{error}</div>
		);
	}

	if (!data) return null;

	// Transform API response into view props
	const title = data?.title ?? '';
	const currency = data?.currency;
	const status = data?.status;
	const trackingId = data?.trackingId;
	const lastUpdatedOn = data?.lastUpdatedOn; // optional if backend provides
	const expenses = Array.isArray(data?.expenses) ? data.expenses.map(e => ({
		description: e?.details,
		amount: e?.amount,
		date: e?.date,
		status: e?.status,
		trackingId: e?.trackingId,
		lastUpdatedOn: e?.lastUpdatedOn,
		rejectionReason: e?.rejectionReason,
	})) : [];
	const rejectionReason = data?.rejectionReason;
	
	// Calculate different amounts based on expense status
	const total = expenses.reduce((sum, e) => sum + (parseFloat(e.amount) || 0), 0);
	const reimbursed = expenses
		.filter(e => e.status === ExpenseStatus.Reimbursed)
		.reduce((sum, e) => sum + (parseFloat(e.amount) || 0), 0);
	const pending = expenses
		.filter(e => e.status === ExpenseStatus.PendingApproval || e.status === ExpenseStatus.PendingReimbursement)
		.reduce((sum, e) => sum + (parseFloat(e.amount) || 0), 0);
	const rejected = expenses
		.filter(e => e.status === ExpenseStatus.Rejected)
		.reduce((sum, e) => sum + (parseFloat(e.amount) || 0), 0);

	const handleEdit = () => {
		navigate(`/form/${effectiveFormId}/edit`);
	};

	// Check if form is editable based on status
	const isFormEditable = (formStatus) => {
		return formStatus === FormStatus.PendingApproval || formStatus === FormStatus.Rejected;
	};

	return (
		<div>
			<div className="d-flex justify-content-between align-items-center mb-3">
				<h4 className="mb-0">Expense Form Details</h4>
				{isFormEditable(status) && (
					<button 
						className="btn btn-primary"
						onClick={handleEdit}
					>
						<i className="bi bi-pencil-square me-1"></i>
						Edit Form
					</button>
				)}
			</div>
			<ExpenseSummaryCard
				title={title}
				currency={currency}
				totalAmount={total}
				reimbursedAmount={reimbursed}
				pendingAmount={pending}
				rejectedAmount={rejected}
				status={status}
				trackingId={trackingId}
				lastUpdatedOn={lastUpdatedOn}
				rejectionReason={rejectionReason}
			/>
			<ExpenseList expenses={expenses} currency={currency}  />
		</div>
	);
}
