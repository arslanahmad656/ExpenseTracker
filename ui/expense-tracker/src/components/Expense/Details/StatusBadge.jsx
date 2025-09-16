import React from 'react';
import { ExpenseStatus, StatusToReadableString } from '../../../utils/enums';

const STATUS_TO_CLASS = {
	[ExpenseStatus.PendingApproval]: 'bg-warning text-dark',
	[ExpenseStatus.PendingReimbursement]: 'bg-info text-dark',
	[ExpenseStatus.Rejected]: 'bg-danger',
	[ExpenseStatus.Reimbursed]: 'bg-success',
	[ExpenseStatus.Cancelled]: 'bg-secondary',
};

export default function StatusBadge({ status }) {
	if (!status) return null;
	const cls = STATUS_TO_CLASS[status] || 'bg-secondary';
	const readable = StatusToReadableString(status);
	const isTerminalSuccess = status === ExpenseStatus.Reimbursed;
	const isTerminalCancelled = status === ExpenseStatus.Cancelled;
	const prefix = isTerminalSuccess ? '✓ ' : isTerminalCancelled ? '⛔ ' : '';
	const title = isTerminalSuccess
		? 'Terminal (successful)'
		: isTerminalCancelled
			? 'Terminal (cancelled)'
			: undefined;
	return (
		<span className={`badge ${cls} rounded-pill`} title={title}>
			{prefix}{readable}
		</span>
	);
}
