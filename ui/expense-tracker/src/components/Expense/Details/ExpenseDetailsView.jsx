import React, { useEffect, useState } from 'react';
import ExpenseSummaryCard from './ExpenseSummaryCard';
import ExpenseList from './ExpenseList';
import formService from '../../../api/formService';
import { useParams } from 'react-router-dom';

export default function ExpenseDetailsView({ formId }) {
	const params = useParams();
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
	const currency = data?.currency ?? null;
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
	const total = expenses.reduce((sum, e) => sum + (parseFloat(e.amount) || 0), 0);

	return (
		<div>
			<ExpenseSummaryCard
				title={title}
				currency={currency}
				totalAmount={total}
				status={status}
				trackingId={trackingId}
				lastUpdatedOn={lastUpdatedOn}
				rejectionReason={rejectionReason}
			/>
			<ExpenseList expenses={expenses} currency={currency}  />
		</div>
	);
}
