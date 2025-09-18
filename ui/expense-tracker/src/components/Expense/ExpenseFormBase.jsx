import React, { useState, useEffect } from 'react';
import CurrencySelect from './CurrencySelect';
import ExpenseItemForm from './ExpenseItemForm';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { getCallback } from '../../utils/callbackRegistry';
import { FormStatus, ExpenseStatus } from '../../utils/enums';
import formService from '../../api/formService';
import { endPoints } from '../../utils/endPoints';

// Helper function to convert datetime to local date input format without timezone issues
const toLocalDateInput = (dateString) => {
    if (!dateString) return '';
    
    // Handle timezone offset by parsing the date in the original timezone
    // Extract the date part directly from the ISO string to avoid timezone conversion
    if (dateString.includes('T')) {
        // For ISO strings like "2025-09-18T00:00:00+05:00", extract just the date part
        return dateString.split('T')[0];
    }
    
    // Fallback for other date formats
    const date = new Date(dateString);
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
};

export default function ExpenseFormBase({
    mode = 'create', // 'create' or 'update'
    formId: propFormId,
    initialData = null,
    title = 'Expense Form',
    submitButtonText = 'Submit',
    canUpdateForm = true,
    canUpdateExpense = () => true
}) {
    const { state } = useLocation();
    const params = useParams();
    const navigate = useNavigate();
    const effectiveFormId = propFormId ?? params?.formId ?? params?.id;
    
    const [loading, setLoading] = useState(mode === 'update');
    const [error, setError] = useState('');
    const [formData, setFormData] = useState(initialData);
    const [titleValue, setTitleValue] = useState(initialData?.title ?? state?.title ?? '');
    const [currency, setCurrency] = useState(initialData?.currency?.code ?? state?.currency?.code ?? state?.currency ?? '');
    const [expenses, setExpenses] = useState(
        initialData?.expenses 
            ? initialData.expenses.map(e => ({
                id: e?.id,
                description: e?.details ?? e?.description ?? '',
                amount: e?.amount?.toString() ?? '',
                date: toLocalDateInput(e?.date),
                status: e?.status ?? ExpenseStatus.PendingApproval,
                trackingId: e?.trackingId,
                lastUpdatedOn: e?.lastUpdatedOn,
                rejectionReason: e?.rejectionReason,
            }))
            : state?.expenses?.map(e => ({
                id: e?.id,
                description: e?.description ?? '',
                amount: e?.amount?.toString() ?? '',
                date: e?.date ?? new Date().toISOString().slice(0, 10),
                status: e?.status ?? ExpenseStatus.PendingApproval,
                trackingId: e?.trackingId,
                lastUpdatedOn: e?.lastUpdatedOn,
                rejectionReason: e?.rejectionReason,
            })) ?? [{ 
                description: '', 
                amount: '', 
                date: new Date().toISOString().slice(0, 10),
                status: ExpenseStatus.PendingApproval
            }]
    );
    const [formStatus, setFormStatus] = useState(initialData?.status ?? state?.status);
    const [errors, setErrors] = useState({});
    const [submitError, setSubmitError] = useState('');
    const [showCancelConfirm, setShowCancelConfirm] = useState(false);
    const [isCancelling, setIsCancelling] = useState(false);
    const [showCancelExpenseConfirm, setShowCancelExpenseConfirm] = useState(false);
    const [expenseToCancel, setExpenseToCancel] = useState(null);
    const [cancellationReason, setCancellationReason] = useState('');
    const [formCancellationReason, setFormCancellationReason] = useState('');

    // Add global error handler for unhandled promise rejections
    useEffect(() => {
        const handleUnhandledRejection = (event) => {
            console.error('Unhandled promise rejection:', event.reason);
            setSubmitError('An unexpected error occurred. Please try again.');
            event.preventDefault(); // Prevent the default browser error handling
        };

        window.addEventListener('unhandledrejection', handleUnhandledRejection);
        
        return () => {
            window.removeEventListener('unhandledrejection', handleUnhandledRejection);
        };
    }, []);

    // Load form data for update mode
    useEffect(() => {
        if (mode !== 'update' || !effectiveFormId || initialData) return;
        
        let isMounted = true;
        async function loadForm() {
            setLoading(true);
            setError('');
            try {
                const result = await formService.getDetailedForm(effectiveFormId);
                if (!isMounted) return;
                
                setFormData(result);
                setTitleValue(result?.title ?? '');
                setCurrency(result?.currency?.code ?? '');
                setFormStatus(result?.status);
                
                const transformedExpenses = Array.isArray(result?.expenses) 
                    ? result.expenses.map(e => ({
                        id: e?.id,
                        description: e?.details ?? '',
                        amount: e?.amount?.toString() ?? '',
                        date: toLocalDateInput(e?.date),
                        status: e?.status,
                        trackingId: e?.trackingId,
                        lastUpdatedOn: e?.lastUpdatedOn,
                        rejectionReason: e?.rejectionReason,
                    }))
                    : [];
                setExpenses(transformedExpenses);
            } catch (err) {
                if (!isMounted) return;
                setError(err?.message || 'Failed to load form details');
            } finally {
                if (isMounted) setLoading(false);
            }
        }
        loadForm();
        return () => { isMounted = false; };
    }, [mode, effectiveFormId, initialData]);

    const getRowErrors = (it) => {
        const rowErrors = {};
        if (!it.description?.trim()) rowErrors.description = 'Required';
        if (!it.amount || isNaN(Number(it.amount))) rowErrors.amount = 'Valid amount required';
        if (!it.date) rowErrors.date = 'Required';
        return rowErrors;
    };

    const isExpenseValid = (it) => it.description?.trim() && it.amount && !isNaN(Number(it.amount)) && it.date;

    const addExpense = () => {
        if (!canUpdateForm) return;
        if (expenses.some((e) => !isExpenseValid(e))) {
            setErrors((prev) => ({ ...prev, expenses: 'Complete existing expenses before adding a new one' }));
            return;
        }
        setErrors((prev) => ({ ...prev, expenses: '' }));
        const today = new Date().toISOString().slice(0, 10);
        setExpenses(prev => [...prev, { 
            id: null, 
            description: '', 
            amount: '', 
            date: today,
            status: ExpenseStatus.PendingApproval,
            trackingId: null,
            lastUpdatedOn: null,
            rejectionReason: null
        }]);
    };

    const removeExpense = (index) => {
        if (!canUpdateForm) return;
        setExpenses(prev => prev.filter((_, i) => i !== index));
    };

    const cancelExpense = (index) => {
        if (!canUpdateForm) return;
        setExpenses(prev => prev.map((expense, i) => 
            i === index 
                ? { ...expense, status: ExpenseStatus.Cancelled }
                : expense
        ));
    };


    const handleCancelForm = () => {
        if (!canUpdateForm || !effectiveFormId) return;
        setFormCancellationReason('');
        setShowCancelConfirm(true);
    };

    const confirmCancelForm = async () => {
        if (!formCancellationReason.trim()) return; // Don't proceed if reason is empty
        
        if (effectiveFormId) {
            setIsCancelling(true);
            setShowCancelConfirm(false);
            
            try {
                debugger;
                await formService.cancelForm(effectiveFormId, formCancellationReason.trim());
                console.log('Form cancelled successfully');
                // Redirect to details page after successful cancellation
                navigate(`/form/${effectiveFormId}/details`);
            } catch (err) {
                console.error('Form cancellation error:', err);
                setSubmitError(err.message || 'Failed to cancel form. Please try again.');
                setIsCancelling(false); // Reset cancelling state on error
            }
        }
    };

    const cancelCancelForm = () => {
        setShowCancelConfirm(false);
        setFormCancellationReason('');
    };

    const handleCancelExpense = (expense) => {
        debugger;
        if (!canUpdateForm || !expense.id) return;
        setExpenseToCancel(expense);
        setCancellationReason('');
        setShowCancelExpenseConfirm(true);
    };

    const confirmCancelExpense = async () => {
        debugger;
        if (!cancellationReason.trim()) return; // Don't proceed if reason is empty
        
        if (expenseToCancel) {
            debugger;
            try {
                await formService.cancelExpense(expenseToCancel.id, cancellationReason.trim());
                console.log('Expense cancelled successfully');
                debugger;
                // Mark expense as cancelled immediately after successful API call
                setExpenses(prev => prev.map(expense => 
                    expense.id === expenseToCancel.id 
                        ? { ...expense, status: ExpenseStatus.Cancelled, rejectionReason: cancellationReason.trim() }
                        : expense
                ));
                
            } catch (err) {
                console.error('Expense cancellation error:', err);
                setSubmitError(err.message || 'Failed to cancel expense. Please try again.');
                setShowCancelExpenseConfirm(false);
                setExpenseToCancel(null);
                setCancellationReason('');
                return; // Don't update UI if API call failed
            }
        }
        
        setShowCancelExpenseConfirm(false);
        setExpenseToCancel(null);
        setCancellationReason('');
    };

    const cancelCancelExpense = () => {
        setShowCancelExpenseConfirm(false);
        setExpenseToCancel(null);
        setCancellationReason('');
    };

    const updateExpense = (index, updated) => {
        if (!canUpdateForm) return;
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
        if (!titleValue.trim()) newErrors.title = 'Title is required';
        if (!currency) newErrors.currency = 'Currency is required';
        if (expenses.length === 0) newErrors.expenses = 'Add at least one expense';
        expenses.forEach((it, i) => {
            const rowErrors = {};
            if (!it.description?.trim()) rowErrors.description = 'Required';
            if (!it.amount || isNaN(Number(it.amount))) rowErrors.amount = 'Valid amount required';
            if (!it.date) rowErrors.date = 'Required';
            if (Object.keys(rowErrors).length) newErrors[`expense_${i}`] = rowErrors;
        });
        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e) => {
        debugger;
        e.preventDefault();
        setSubmitError('');
        if (!validate()) return;
        
        try {
            
            if (mode === 'create') {
                const createFormPayload = {
                    Form: {
                        Title: titleValue,
                        CurrencyCode: currency
                    },
                    Expenses: expenses.map(e => ({
                        Description: e.description,
                        Amount: Number(e.amount),
                        ExpenseDate: e.date
                    }))
                };

                const response = await formService.submitExpenseForm(createFormPayload);
                debugger;
                navigate(endPoints.getDetailedForm(response.data));
            } else if (mode === 'update') {
                debugger;
                const updateFormPayload = {
                    Form: {
                        Id: effectiveFormId,
                        Title: titleValue,
                        CurrencyCode: currency
                    },
                    Expenses: expenses.map(e => ({
                        Id: e.id ?? 0,
                        Description: e.description,
                        Amount: Number(e.amount),
                        ExpenseDate: e.date
                    }))
                };
                 await formService.updateForm(effectiveFormId, updateFormPayload);
                 console.log('Form updated successfully');
                 // Reload the page after successful update
                 window.location.reload();
              }
              
              // Success - you can add navigation or success message here
              console.log('Form submitted successfully');
            
        } catch (err) {
            console.error('Form submission error:', err);
            setSubmitError(err.message || 'Failed to submit form. Please try again.');
        }
    };

    if (loading) {
        return (
            <div className="card shadow-sm border-0">
                <div className="card-body p-4">
                    <div className="text-center">Loading...</div>
                </div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="alert alert-danger" role="alert">{error}</div>
        );
    }

    // Check if form is editable based on status
    const isFormEditable = (formStatus) => {
        return formStatus === FormStatus.PendingApproval || formStatus === FormStatus.Rejected;
    };

    // Show non-editable message for certain form statuses
    if (mode === 'update' && formStatus && !isFormEditable(formStatus)) {
        const getNonEditableMessage = (status) => {
            switch (status) {
                case FormStatus.Cancelled:
                    return {
                        title: 'Form Cancelled',
                        message: 'This expense form has been cancelled and is no longer editable.',
                        icon: 'bi-x-circle',
                        color: 'text-secondary'
                    };
                case FormStatus.Reimbursed:
                    return {
                        title: 'Form Reimbursed',
                        message: 'This expense form has been fully reimbursed and is no longer editable.',
                        icon: 'bi-check-circle',
                        color: 'text-success'
                    };
                case FormStatus.PendingReimbursement:
                    return {
                        title: 'Form Under Review',
                        message: 'This expense form is pending reimbursement approval and cannot be edited.',
                        icon: 'bi-clock',
                        color: 'text-info'
                    };
                default:
                    return {
                        title: 'Form Not Editable',
                        message: 'This expense form is not editable in its current state.',
                        icon: 'bi-lock',
                        color: 'text-muted'
                    };
            }
        };

        const messageInfo = getNonEditableMessage(formStatus);
        
        return (
            <div className="card shadow-sm border-0">
                <div className="card-body p-5 text-center">
                    <i className={`bi ${messageInfo.icon} ${messageInfo.color} mb-3`} style={{ fontSize: '3rem' }}></i>
                    <h4 className={`${messageInfo.color} mb-3`}>{messageInfo.title}</h4>
                    <p className="text-muted mb-4">{messageInfo.message}</p>
                    <button 
                        className="btn btn-outline-primary"
                        onClick={() => navigate(`/form/${effectiveFormId}/details`)}
                    >
                        <i className="bi bi-eye me-1"></i>
                        View Details
                    </button>
                </div>
            </div>
        );
    }

    // Calculate total excluding rejected and cancelled expenses
    const totalAmount = expenses
        .filter(e => e.status !== ExpenseStatus.Rejected && e.status !== ExpenseStatus.Cancelled)
        .reduce((sum, e) => sum + (parseFloat(e.amount) || 0), 0).toFixed(2);
    
    // Calculate total including all expenses for comparison
    const totalAllAmount = expenses.reduce((sum, e) => sum + (parseFloat(e.amount) || 0), 0).toFixed(2);
    
    const currencyCode = formData?.currency?.code || currency || 'CUR';
    
    // Check if expense is locked (cannot be edited)
    const isExpenseLocked = (expenseStatus) => 
        expenseStatus === ExpenseStatus.PendingReimbursement || 
        expenseStatus === ExpenseStatus.Reimbursed || 
        expenseStatus === ExpenseStatus.Cancelled;

    // Show loading indicator when cancelling
    if (isCancelling) {
        return (
            <div className="card shadow-sm border-0">
                <div className="card-body p-4">
                    <div className="text-center py-5">
                        <div className="spinner-border text-warning mb-3" role="status">
                            <span className="visually-hidden">Loading...</span>
                        </div>
                        <h5 className="text-warning">Cancelling form...</h5>
                        <p className="text-muted">Please wait while we process your request.</p>
                    </div>
                </div>
            </div>
        );
    }

    return (
        <div className="card shadow-sm border-0">
            <div className="card-body p-4">
                <h5 className="card-title mb-3">{title}</h5>

                {!canUpdateForm && (
                    <div className="alert alert-warning" role="alert">
                        This form cannot be updated. Only forms with status "Pending Approval" or "Rejected" can be updated.
                    </div>
                )}

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
                                value={titleValue}
                                onChange={(e) => {
                                    const v = e.target.value;
                                    setTitleValue(v);
                                    if (errors.title && v.trim()) {
                                        setErrors(prev => { const n = { ...prev }; delete n.title; return n; });
                                    }
                                    if (submitError) {
                                        setSubmitError('');
                                    }
                                }}
                                placeholder="e.g., Team Outing"
                                readOnly={!canUpdateForm}
                                disabled={!canUpdateForm}
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
                                    if (submitError) {
                                        setSubmitError('');
                                    }
                                }}
                            />
                            {errors.currency && <div className="text-danger small mt-1">{errors.currency}</div>}
                        </div>
                    </div>

                    <div className="d-flex justify-content-between align-items-center mb-2">
                        <h6 className="mb-0">Expenses</h6>
                        <div>
                            {canUpdateForm && (
                                <>
                                    <button 
                                        type="button" 
                                        className="btn btn-outline-primary btn-sm me-2" 
                                        onClick={addExpense}
                                    >
                                        <i className="bi bi-plus-lg me-1"></i> Add Expense
                                    </button>
                                    {mode === 'update' && effectiveFormId && (
                                        <button 
                                            type="button" 
                                            className="btn btn-outline-warning btn-sm" 
                                            onClick={handleCancelForm}
                                        >
                                            <i className="bi bi-x-circle me-1"></i> Cancel Form
                                        </button>
                                    )}
                                </>
                            )}
                        </div>
                    </div>
                    {errors.expenses && <div className="text-danger small mb-2">{errors.expenses}</div>}

                    {expenses.map((item, index) => {
                        const isNewExpense = !item.id; // New expenses don't have an id
                        const canUpdateThisExpense = canUpdateExpense(item.status);
                        const isLocked = isExpenseLocked(item.status);
                        
                        // Get lock color and tooltip based on status
                        const getLockInfo = (status) => {
                            switch (status) {
                                case ExpenseStatus.PendingReimbursement:
                                    return { color: 'text-info', tooltip: 'Locked - Pending reimbursement approval' };
                                case ExpenseStatus.Reimbursed:
                                    return { color: 'text-success', tooltip: 'Locked - Already reimbursed' };
                                case ExpenseStatus.Cancelled:
                                    return { color: 'text-secondary', tooltip: 'Locked - Expense cancelled' };
                                default:
                                    return { color: 'text-muted', tooltip: 'This expense cannot be edited' };
                            }
                        };
                        
                        const lockInfo = isLocked ? getLockInfo(item.status) : null;
                        
                        return (
                            <div key={index} className="position-relative">
                                {isLocked && lockInfo && (
                                    <div className="position-absolute top-0 end-0 p-2" style={{ zIndex: 10 }}>
                                        <i className={`bi bi-lock-fill ${lockInfo.color}`} title={lockInfo.tooltip}></i>
                                    </div>
                                )}
                                <ExpenseItemForm
                                    index={index}
                                    item={item}
                                    onChange={updateExpense}
                                    onDelete={isNewExpense ? removeExpense : (() => handleCancelExpense(item))}
                                    errors={errors[`expense_${index}`] || {}}
                                    readOnly={!canUpdateForm || (!isNewExpense && !canUpdateThisExpense) || isLocked}
                                    showCancelButton={!isNewExpense && mode === 'update' && canUpdateForm && canUpdateThisExpense && !isLocked}
                                />
                            </div>
                        );
                    })}

                    <div className="d-flex justify-content-between align-items-center mt-3">
                        <div className="d-flex flex-column">
                            <div className="d-flex align-items-center mb-1">
                                <span className="text-muted me-2 fw-semibold">Total</span>
                                <span className="px-3 py-2 rounded-pill bg-light border fw-bold fs-5">
                                    {currencyCode} {totalAmount}
                                </span>
                            </div>
                            <small className="text-muted">
                                <i className="bi bi-info-circle me-1"></i>
                                Excludes rejected and cancelled expenses.
                            </small>
                        </div>
                        {canUpdateForm && (
                            <button type="submit" className="btn btn-primary" disabled={expenses.length === 0}>
                                {submitButtonText}
                            </button>
                        )}
                    </div>
                </form>

                {/* Cancel Form Confirmation Modal */}
                {showCancelConfirm && (
                    <div className="modal show d-block" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
                        <div className="modal-dialog modal-dialog-centered">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h5 className="modal-title">Cancel Form</h5>
                                    <button 
                                        type="button" 
                                        className="btn-close" 
                                        onClick={cancelCancelForm}
                                    ></button>
                                </div>
                                <div className="modal-body">
                                    <p>Are you sure you want to cancel this expense form? This action cannot be undone.</p>
                                    <p className="text-muted small">All expenses will be marked as cancelled and the form will be closed.</p>
                                    <div className="mb-3">
                                        <label htmlFor="formCancellationReason" className="form-label">
                                            Cancellation Reason <span className="text-danger">*</span>
                                        </label>
                                        <textarea
                                            id="formCancellationReason"
                                            className="form-control"
                                            rows="3"
                                            value={formCancellationReason}
                                            onChange={(e) => setFormCancellationReason(e.target.value)}
                                            placeholder="Please provide a reason for cancelling this form..."
                                            required
                                        />
                                        {!formCancellationReason.trim() && (
                                            <div className="invalid-feedback d-block">
                                                Cancellation reason is required.
                                            </div>
                                        )}
                                    </div>
                                </div>
                                <div className="modal-footer">
                                    <button 
                                        type="button" 
                                        className="btn btn-secondary" 
                                        onClick={cancelCancelForm}
                                    >
                                        Keep Form
                                    </button>
                                    <button 
                                        type="button" 
                                        className="btn btn-warning" 
                                        onClick={confirmCancelForm}
                                        disabled={!formCancellationReason.trim()}
                                    >
                                        <i className="bi bi-x-circle me-1"></i> Cancel Form
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                )}

                {/* Cancel Expense Confirmation Modal */}
                {showCancelExpenseConfirm && expenseToCancel && (
                    <div className="modal show d-block" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
                        <div className="modal-dialog modal-dialog-centered">
                            <div className="modal-content">
                                <div className="modal-header">
                                    <h5 className="modal-title">Cancel Expense</h5>
                                    <button 
                                        type="button" 
                                        className="btn-close" 
                                        onClick={cancelCancelExpense}
                                    ></button>
                                </div>
                                <div className="modal-body">
                                    <p>Are you sure you want to cancel this expense? This action cannot be undone.</p>
                                    <div className="mb-3">
                                        <label htmlFor="cancellationReason" className="form-label">
                                            Cancellation Reason <span className="text-danger">*</span>
                                        </label>
                                        <textarea
                                            id="cancellationReason"
                                            className="form-control"
                                            rows="3"
                                            value={cancellationReason}
                                            onChange={(e) => setCancellationReason(e.target.value)}
                                            placeholder="Please provide a reason for cancelling this expense..."
                                            required
                                        />
                                        {!cancellationReason.trim() && (
                                            <div className="invalid-feedback d-block">
                                                Cancellation reason is required.
                                            </div>
                                        )}
                                    </div>
                                </div>
                                <div className="modal-footer">
                                    <button 
                                        type="button" 
                                        className="btn btn-secondary" 
                                        onClick={cancelCancelExpense}
                                    >
                                        Keep Expense
                                    </button>
                                    <button 
                                        type="button" 
                                        className="btn btn-danger" 
                                        onClick={confirmCancelExpense}
                                        disabled={!cancellationReason.trim()}
                                    >
                                        <i className="bi bi-x-lg me-1"></i> Cancel Expense
                                    </button>
                                </div>
                            </div>
                        </div>
                    </div>
                )}

            </div>
        </div>
    );
}
