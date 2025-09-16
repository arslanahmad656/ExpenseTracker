import React from 'react';
import ExpenseListItem from './ExpenseListItem';

export default function ExpenseList({ expenses = [], currency }) {
	if (!Array.isArray(expenses)) expenses = [];
	return (
		<div className="card border-0 shadow-sm">
			<div className="card-header bg-white">
				<h6 className="mb-0">Expenses</h6>
			</div>
			<div className="list-group list-group-flush">
				{expenses.length === 0 && (
					<div className="list-group-item text-muted">No expenses</div>
				)}
				{expenses.map((e, i) => (
					<ExpenseListItem key={i} expense={e} currency={currency?.symbol} />
				))}
			</div>
		</div>
	);
}
