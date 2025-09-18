import React from 'react';
import StatusBadge from './StatusBadge';
import { FormStatus } from '../../../utils/enums';

export default function ExpenseSummaryCard({
	title,
	currency,
	totalAmount,
	reimbursedAmount,
	pendingAmount,
	rejectedAmount,
	status,
	trackingId,
	lastUpdatedOn,
	rejectionReason,
}) {
	console.log('props received by expense summary card', arguments);
	const isRejected = status === FormStatus.Rejected;
	return (
		<div className="card border-0 shadow-sm mb-3">
			<div className="card-body">
				<div className="d-flex justify-content-between align-items-start mb-2">
					<div>
						<h5 className="card-title mb-1">{title}</h5>
						<div className="text-muted small">Currency: {currency?.code || '—'}</div>
					</div>
					<StatusBadge status={status} />
				</div>

				{isRejected && rejectionReason && (
					<div className="alert alert-danger py-2">
						<strong>Rejection Reason:</strong> {rejectionReason}
					</div>
				)}

				<div className="row g-3">
					<div className="col-md-3">
						<div className="text-muted small">Tracking Id</div>
						<div className="fw-semibold">{trackingId || '—'}</div>
					</div>
					<div className="col-md-3">
						<div className="text-muted small">Last Updated On</div>
						<div className="fw-semibold">{new Date(lastUpdatedOn).toLocaleString() || '—'}</div>
					</div>
					<div className="col-md-6">
						<div className="text-muted small mb-2">Amount Breakdown</div>
						<div className="row g-2">
							<div className="col-6">
								<div className="text-muted small">Total</div>
								<div className="fw-bold">
									{currency?.symbol || 'CUR'} {Number(totalAmount || 0).toFixed(2)}
								</div>
							</div>
							<div className="col-6">
								<div className="text-muted small">Reimbursed</div>
								<div className="fw-bold text-success">
									{currency?.symbol || 'CUR'} {Number(reimbursedAmount || 0).toFixed(2)}
								</div>
							</div>
							<div className="col-6">
								<div className="text-muted small">Pending</div>
								<div className="fw-bold text-warning">
									{currency?.symbol || 'CUR'} {Number(pendingAmount || 0).toFixed(2)}
								</div>
							</div>
							<div className="col-6">
								<div className="text-muted small">Rejected</div>
								<div className="fw-bold text-danger">
									{currency?.symbol || 'CUR'} {Number(rejectedAmount || 0).toFixed(2)}
								</div>
							</div>
						</div>
						<div className="mt-2">
							<small className="text-muted">
								<i className="bi bi-info-circle me-1"></i>
								All amounts exclude cancelled expenses from calculations.
							</small>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
}
