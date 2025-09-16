import React from 'react';
import ExpenseSummaryCard from './ExpenseSummaryCard';
import ExpenseList from './ExpenseList';

export default function ExpenseDetailsView({
	title,
	currency,
	status,
	trackingId,
	lastUpdatedOn,
	expenses = [],
	rejectionReason
}) {
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
