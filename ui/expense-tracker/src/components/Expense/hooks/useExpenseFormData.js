import { useState, useEffect } from 'react';
import { useLocation, useParams } from 'react-router-dom';
import { ExpenseStatus } from '../../../utils/enums';
import formService from '../../../api/formService';

const toLocalDateInput = (dateString) => {
    if (!dateString) return '';
    
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

export const useExpenseFormData = (mode, propFormId, initialData) => {
    const { state } = useLocation();
    const params = useParams();
    const effectiveFormId = propFormId ?? params?.formId;
    
    const [loading, setLoading] = useState(mode === 'update' || mode === 'manager' || mode === 'accountant');
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

    // this effect is used to load the form data for update/manager/accountant modes
    useEffect(() => {
        if ((mode !== 'update' && mode !== 'manager' && mode !== 'accountant') || !effectiveFormId || initialData) return;
        
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

    // this function is used to add a new expense
    const addExpense = (canUpdateForm) => {
        if (!canUpdateForm) return;
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

    // this function is used to remove an expense
    const removeExpense = (index, canUpdateForm) => {
        if (!canUpdateForm) return;
        setExpenses(prev => prev.filter((_, i) => i !== index));
    };

    // this function is used to update an expense
    const updateExpense = (index, updated, canUpdateForm) => {
        if (!canUpdateForm) return;
        setExpenses(prev => prev.map((it, i) => (i === index ? updated : it)));
    };

    // // this function is used to cancel an expense
    // const cancelExpense = (index, canUpdateForm) => {
    //     if (!canUpdateForm) return;
    //     setExpenses(prev => prev.map((expense, i) => 
    //         i === index 
    //             ? { ...expense, status: ExpenseStatus.Cancelled }
    //             : expense
    //     ));
    // };

    // this function is used to mark an expense as cancelled
    const markExpenseAsCancelled = (expenseId, rejectionReason) => {
        setExpenses(prev => prev.map(expense => 
            expense.id === expenseId 
                ? { ...expense, status: ExpenseStatus.Cancelled, rejectionReason }
                : expense
        ));
    };

    return {
        // State
        loading,
        error,
        formData,
        titleValue,
        currency,
        expenses,
        formStatus,
        effectiveFormId,
        
        // Setters
        setTitleValue,
        setCurrency,
        setError,
        
        // Actions
        addExpense,
        removeExpense,
        updateExpense,
        // cancelExpense,
        markExpenseAsCancelled,
    };
};
