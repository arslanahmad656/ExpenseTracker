import React from 'react';
import StatusBadge from './StatusBadge';
import { ExpenseStatus } from '../../../utils/enums';

export default function ExpenseListItem({ expense, currency }) {
	const { description, amount, date, status, trackingId, lastUpdatedOn, rejectionReason } = expense || {};
	const isRejected = status === ExpenseStatus.Rejected;
	return (
		<div className="list-group-item py-3">
			<div className="d-flex justify-content-between align-items-start">
				<div>
					<div className="fw-semibold">{description || '—'}</div>
					<div className="text-muted small">{date || '—'}</div>
				</div>
				<div className="text-end">
					<div className="fw-bold">{currency || 'CUR'} {Number(amount || 0).toFixed(2)}</div>
					<StatusBadge status={status} />
				</div>
			</div>
			{isRejected && rejectionReason && (
				<div className="alert alert-danger py-2 mt-2 mb-0">
					<strong>Rejection Reason:</strong> {rejectionReason}
				</div>
			)}
			<div className="row g-2 mt-2 text-muted small">
				<div className="col-md-6">Expense Tracking Id: {trackingId || '—'}</div>
				<div className="col-md-6 text-md-end">Last Updated On: {lastUpdatedOn || '—'}</div>
			</div>
		</div>
	);
}
