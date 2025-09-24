import { useState } from 'react';

export const useExpenseFormModals = () => {
    // Form cancellation modal
    const [showCancelConfirm, setShowCancelConfirm] = useState(false);
    const [isCancelling, setIsCancelling] = useState(false);
    const [formCancellationReason, setFormCancellationReason] = useState('');

    // Expense cancellation modal
    const [showCancelExpenseConfirm, setShowCancelExpenseConfirm] = useState(false);
    const [expenseToCancel, setExpenseToCancel] = useState(null);
    const [expenseCancellationReason, setExpenseCancellationReason] = useState('');

    // Form rejection modal (manager mode)
    const [showRejectFormConfirm, setShowRejectFormConfirm] = useState(false);
    const [formRejectionReason, setFormRejectionReason] = useState('');

    // Form approval modal (manager mode)
    const [showApproveFormConfirm, setShowApproveFormConfirm] = useState(false);

    // Form reimbursement modal (accountant mode)
    const [showAccountantApproveFormConfirm, setShowAccountantApproveFormConfirm] = useState(false);

    // Form cancellation modal functions
    const openCancelFormModal = () => {
        setFormCancellationReason('');
        setShowCancelConfirm(true);
    };

    const closeCancelFormModal = () => {
        setShowCancelConfirm(false);
        setFormCancellationReason('');
    };

    // Expense cancellation modal functions
    const openCancelExpenseModal = (expense) => {
        setExpenseToCancel(expense);
        setExpenseCancellationReason('');
        setShowCancelExpenseConfirm(true);
    };

    const closeCancelExpenseModal = () => {
        setShowCancelExpenseConfirm(false);
        setExpenseToCancel(null);
        setExpenseCancellationReason('');
    };

    // Form rejection modal functions (manager mode)
    const openRejectFormModal = () => {
        setFormRejectionReason('');
        setShowRejectFormConfirm(true);
    };

    const closeRejectFormModal = () => {
        setShowRejectFormConfirm(false);
        setFormRejectionReason('');
    };

    // Form approval modal functions (manager mode)
    const openApproveFormModal = () => {
        setShowApproveFormConfirm(true);
    };

    const closeApproveFormModal = () => {
        setShowApproveFormConfirm(false);
    };

    // Form reimbursement modal functions (accountant mode)
    const openReimburseFormModal = () => {
        setShowAccountantApproveFormConfirm(true);
    };

    const closeReimburseFormModal = () => {
        setShowAccountantApproveFormConfirm(false);
    };

    return {
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
    };
};
