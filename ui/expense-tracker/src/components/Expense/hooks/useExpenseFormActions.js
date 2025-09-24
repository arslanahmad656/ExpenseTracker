import { useNavigate } from 'react-router-dom';
import { endPoints } from '../../../utils/endPoints';
import formService from '../../../api/formService';

export const useExpenseFormActions = () => {
    const navigate = useNavigate();

    // this function is used to submit (initial creation or later update) of the expense form
    const handleSubmit = async (mode, titleValue, currency, expenses, effectiveFormId, validate, setSubmitError) => {
        setSubmitError('');
        if (!validate(titleValue, currency, expenses)) return;
        
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
                navigate(endPoints.getDetailedForm(response.data));
            } else if (mode === 'update') {
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
                navigate(`/form/${effectiveFormId}/details`);
            }
            
            
        } catch (err) {
            setSubmitError(err.message || 'Failed to submit form. Please try again.');
        }
    };

    const handleCancelForm = async (effectiveFormId, formCancellationReason, setSubmitError) => {
        if (!formCancellationReason.trim()) return;
        
        try {
            await formService.cancelForm(effectiveFormId, formCancellationReason.trim());
            navigate(`/form/${effectiveFormId}/details`);
        } catch (err) {
            setSubmitError(err.message || 'Failed to cancel form. Please try again.');
        }
    };

    const handleCancelExpense = async (expenseId, cancellationReason, setSubmitError) => {
        if (!cancellationReason.trim()) return;
        
        try {
            await formService.cancelExpense(expenseId, cancellationReason.trim());
        } catch (err) {
            setSubmitError(err.message || 'Failed to cancel expense. Please try again.');
        }
    };

    const handleRejectForm = async (effectiveFormId, formRejectionReason, setSubmitError) => {
        if (!formRejectionReason.trim()) return;
        
        try {
            await formService.rejectForm(effectiveFormId, formRejectionReason.trim());
            navigate('/form/list/Manager');
        } catch (err) {
            setSubmitError(err.message || 'Failed to reject form. Please try again.');
        }
    };

    const handleApproveForm = async (effectiveFormId, setSubmitError) => {
        try {
            await formService.approveForm(effectiveFormId);
            navigate('/form/list/Manager');
        } catch (err) {
            setSubmitError(err.message || 'Failed to approve form. Please try again.');
        }
    };

    const handleReimburseForm = async (effectiveFormId, setSubmitError) => {
        try {
            await formService.reimburseForm(effectiveFormId);
            navigate('/form/list/Accountant');
        } catch (err) {
            setSubmitError(err.message || 'Failed to reimburse form. Please try again.');
        }
    };

    return {
        handleSubmit,
        handleCancelForm,
        handleCancelExpense,
        handleRejectForm,
        handleApproveForm,
        handleReimburseForm,
    };
};
