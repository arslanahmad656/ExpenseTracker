import React, { useEffect } from 'react';
import { FormStatus, ExpenseStatus } from '../../utils/enums';
import { useExpenseFormData } from './hooks/useExpenseFormData';
import { useExpenseFormValidation } from './hooks/useExpenseFormValidation';
import { useExpenseFormActions } from './hooks/useExpenseFormActions';
import { useExpenseFormModals } from './hooks/useExpenseFormModals';
import { ExpenseFormHeader } from './sections/ExpenseFormHeader';
import { FormStatusAlerts } from './sections/FormStatusAlerts';
import { ExpenseListSection } from './sections/ExpenseListSection';
import { FormActionsSection } from './sections/FormActionsSection';
import { FormLoadingStates } from './utils';
import { CancelFormModal } from './modals/CancelFormModal';
import { CancelExpenseModal } from './modals/CancelExpenseModal';
import { RejectFormModal } from './modals/RejectFormModal';
import { ApproveFormModal } from './modals/ApproveFormModal';
import { ReimburseFormModal } from './modals/ReimburseFormModal';

export default function ExpenseFormBase({
    mode = 'create',
    formId: propFormId,
    initialData = null,
    title = 'Expense Form',
    canUpdateForm = true
}) {
    const {
        loading,
        error,
        formData,
        titleValue,
        currency,
        expenses,
        formStatus,
        effectiveFormId,
        setTitleValue,
        setCurrency,
        addExpense,
        removeExpense,
        updateExpense,
        markExpenseAsCancelled,
    } = useExpenseFormData(mode, propFormId, initialData);

    const {
        errors,
        submitError,
        setSubmitError,
        isExpenseValid,
        validate,
        updateExpenseValidation,
        clearFieldError,
        setExpenseError,
        clearExpenseError,
    } = useExpenseFormValidation();

    const {
        handleSubmit: submitForm,
        handleCancelForm: cancelFormAction,
        handleCancelExpense: cancelExpenseAction,
        handleRejectForm: rejectFormAction,
        handleApproveForm: approveFormAction,
        handleReimburseForm: reimburseFormAction,
    } = useExpenseFormActions();

    const {
        // Form cancellation modal
        showCancelConfirm,
        isCancelling,
        formCancellationReason,
        setFormCancellationReason,
        setIsCancelling,
        openCancelFormModal,
        closeCancelFormModal,

        // Expense cancellation modal
        showCancelExpenseConfirm,
        expenseToCancel,
        expenseCancellationReason,
        setExpenseCancellationReason,
        openCancelExpenseModal,
        closeCancelExpenseModal,

        // Form rejection modal (manager mode)
        showRejectFormConfirm,
        formRejectionReason,
        setFormRejectionReason,
        openRejectFormModal,
        closeRejectFormModal,

        // Form approval modal (manager mode)
        showApproveFormConfirm,
        openApproveFormModal,
        closeApproveFormModal,

        // Form reimbursement modal (accountant mode)
        showAccountantApproveFormConfirm,
        openReimburseFormModal,
        closeReimburseFormModal,
    } = useExpenseFormModals();

    // this effect is used to handle unhandled rejection errors
    useEffect(() => {
        const handleUnhandledRejection = (event) => {
            setSubmitError('An unexpected error occurred. Please try again.');
            event.preventDefault();
        };

        window.addEventListener('unhandledrejection', handleUnhandledRejection);
        
        return () => {
            window.removeEventListener('unhandledrejection', handleUnhandledRejection);
        };
    }, [setSubmitError]);

    const handleAddExpense = () => {
        if (!canUpdateForm) return;
        if (expenses.some((e) => !isExpenseValid(e))) {
            setExpenseError('Complete existing expenses before adding a new one');
            return;
        }
        clearExpenseError();
        addExpense(canUpdateForm);
    };

    const handleUpdateExpense = (index, updated) => {
        updateExpense(index, updated, canUpdateForm);
        updateExpenseValidation(index, updated, expenses);
    };

    const handleDeleteExpense = (index) => {
        removeExpense(index, canUpdateForm);
    };

    const handleCancelExpense = (expense) => {
        if (!canUpdateForm || !expense.id) return;
        openCancelExpenseModal(expense);
    };

    const handleCancelForm = () => {
        if (!canUpdateForm || !effectiveFormId) return;
        openCancelFormModal();
    };

    const confirmCancelForm = async () => {
        if (!formCancellationReason.trim()) return;
        
        setIsCancelling(true);
        closeCancelFormModal();
        
        try {
            await cancelFormAction(effectiveFormId, formCancellationReason, setSubmitError);
        } catch (err) {
            setIsCancelling(false);
        }
    };

    const confirmCancelExpense = async () => {
        if (!expenseCancellationReason.trim() || !expenseToCancel) return;
        
        try {
            await cancelExpenseAction(expenseToCancel.id, expenseCancellationReason, setSubmitError);
            markExpenseAsCancelled(expenseToCancel.id, expenseCancellationReason.trim());
            closeCancelExpenseModal();
        } catch (err) {
            closeCancelExpenseModal();
        }
    };

    const handleRejectForm = () => {
        if (!canUpdateForm || !effectiveFormId) return;
        openRejectFormModal();
    };

    const confirmRejectForm = async () => {
        if (!formRejectionReason.trim()) return;
        await rejectFormAction(effectiveFormId, formRejectionReason, setSubmitError);
    };

    const handleApproveForm = () => {
        if (!canUpdateForm || !effectiveFormId) return;
        openApproveFormModal();
    };

    const confirmApproveForm = async () => {
        await approveFormAction(effectiveFormId, setSubmitError);
    };

    const handleAccountantApproveForm = () => {
        if (!canUpdateForm || !effectiveFormId) return;
        openReimburseFormModal();
    };

    const confirmAccountantApproveForm = async () => {
        await reimburseFormAction(effectiveFormId, setSubmitError);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        await submitForm(mode, titleValue, currency, expenses, effectiveFormId, validate, setSubmitError);
    };

    const handleTitleChange = (value, clearError = false) => {
        setTitleValue(value);
        if (clearError && errors.title) {
            clearFieldError('title');
        }
        if (submitError) {
            setSubmitError('');
        }
    };

    const handleCurrencyChange = (value, clearError = false) => {
        setCurrency(value);
        if (clearError && errors.currency) {
            clearFieldError('currency');
        }
        if (submitError) {
            setSubmitError('');
        }
    };

    // Check if form is editable based on status    
    const isFormEditable = (formStatus) => {
        return formStatus === FormStatus.PendingApproval || formStatus === FormStatus.Rejected;
    };

    // Check if form fields should be readonly
    const isFormReadOnly = mode === 'manager' || mode === 'accountant' || (mode === 'update' && !isFormEditable(formStatus));
    
    // Check if form is approved (buttons should be disabled)
    const isFormApproved = mode === 'accountant' 
        ? formStatus === FormStatus.Reimbursed 
        : formStatus === FormStatus.PendingReimbursement || formStatus === FormStatus.Reimbursed;

    // Calculate total excluding only cancelled expenses
    const totalAmount = expenses
        .filter(e => e.status !== ExpenseStatus.Cancelled)
        .reduce((sum, e) => sum + (parseFloat(e.amount) || 0), 0).toFixed(2);
    
    const currencyCode = formData?.currency?.code || currency || 'CUR';

    // Check for special loading/error states first
    const loadingState = FormLoadingStates({
        loading,
        error,
        isCancelling,
        formStatus,
        mode,
        effectiveFormId
    });

    if (loadingState) {
        return loadingState;
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
                    <ExpenseFormHeader
                        titleValue={titleValue}
                        currency={currency}
                        errors={errors}
                        onTitleChange={handleTitleChange}
                        onCurrencyChange={handleCurrencyChange}
                        isReadOnly={isFormReadOnly}
                    />

                    <FormStatusAlerts
                        formStatus={formStatus}
                        formData={formData}
                        mode={mode}
                        isFormApproved={isFormApproved}
                    />

                    <ExpenseListSection
                        expenses={expenses}
                        errors={errors}
                        onAddExpense={handleAddExpense}
                        onUpdateExpense={handleUpdateExpense}
                        onDeleteExpense={handleDeleteExpense}
                        onCancelExpense={handleCancelExpense}
                        onApproveForm={handleApproveForm}
                        onRejectForm={handleRejectForm}
                        onReimburseForm={handleAccountantApproveForm}
                        mode={mode}
                        canUpdateForm={canUpdateForm}
                        isFormReadOnly={isFormReadOnly}
                        isFormApproved={isFormApproved}
                        isExpenseValid={isExpenseValid}
                    />

                    <FormActionsSection
                        mode={mode}
                        canUpdateForm={canUpdateForm}
                        totalAmount={totalAmount}
                        currencyCode={currencyCode}
                        expensesLength={expenses.length}
                        onSubmit={handleSubmit}
                        onCancelForm={handleCancelForm}
                        effectiveFormId={effectiveFormId}
                    />
                </form>

                {/* Modal Components */}
                <CancelFormModal
                    isOpen={showCancelConfirm}
                    formCancellationReason={formCancellationReason}
                    onReasonChange={setFormCancellationReason}
                    onConfirm={confirmCancelForm}
                    onCancel={closeCancelFormModal}
                />

                <CancelExpenseModal
                    isOpen={showCancelExpenseConfirm}
                    expenseToCancel={expenseToCancel}
                    expenseCancellationReason={expenseCancellationReason}
                    onReasonChange={setExpenseCancellationReason}
                    onConfirm={confirmCancelExpense}
                    onCancel={closeCancelExpenseModal}
                />

                <RejectFormModal
                    isOpen={showRejectFormConfirm}
                    formRejectionReason={formRejectionReason}
                    onReasonChange={setFormRejectionReason}
                    onConfirm={confirmRejectForm}
                    onCancel={closeRejectFormModal}
                />

                <ApproveFormModal
                    isOpen={showApproveFormConfirm}
                    onConfirm={confirmApproveForm}
                    onCancel={closeApproveFormModal}
                />

                <ReimburseFormModal
                    isOpen={showAccountantApproveFormConfirm}
                    currencyCode={currencyCode}
                    totalAmount={totalAmount}
                    onConfirm={confirmAccountantApproveForm}
                    onCancel={closeReimburseFormModal}
                />

            </div>
        </div>
    );
}
