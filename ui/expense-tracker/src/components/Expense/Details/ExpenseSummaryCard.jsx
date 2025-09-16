import React from 'react';
import StatusBadge from './StatusBadge';
import { FormStatus } from '../../../utils/enums';

export default function ExpenseSummaryCard({
	title,
	currency,
	totalAmount,
	status,
	trackingId,
	lastUpdatedOn,
	rejectionReason,
}) {
	const isRejected = status === FormStatus.Rejected;
	return (
		<div className="card border-0 shadow-sm mb-3">
			<div className="card-body">
				<div className="d-flex justify-content-between align-items-start mb-2">
					<div>
						<h5 className="card-title mb-1">{title}</h5>
						<div className="text-muted small">Currency: {currency || '—'}</div>
					</div>
					<StatusBadge status={status} />
				</div>

				{isRejected && rejectionReason && (
					<div className="alert alert-danger py-2">
						<strong>Rejection Reason:</strong> {rejectionReason}
					</div>
				)}

				<div className="row g-3">
					<div className="col-md-4">
						<div className="text-muted small">Form Tracking Id</div>
						<div className="fw-semibold">{trackingId || '—'}</div>
					</div>
					<div className="col-md-4">
						<div className="text-muted small">Last Updated On</div>
						<div className="fw-semibold">{lastUpdatedOn || '—'}</div>
					</div>
					<div className="col-md-4">
						<div className="text-muted small">Total Amount</div>
						<div className="fw-bold fs-5">
							{currency || 'CUR'} {Number(totalAmount || 0).toFixed(2)}
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
