import { useState } from 'react';

export const useExpenseFormValidation = () => {
    const [errors, setErrors] = useState({});
    const [submitError, setSubmitError] = useState('');

    // this function is used to get the errors for an expense row
    const getRowErrors = (it) => {
        const rowErrors = {};
        if (!it.description?.trim()) rowErrors.description = 'Required';
        if (!it.amount || isNaN(Number(it.amount))) rowErrors.amount = 'Valid amount required';
        if (!it.date) rowErrors.date = 'Required';
        return rowErrors;
    };

    // this function is used to check if an expense is valid
    const isExpenseValid = (it) => it.description?.trim() && it.amount && !isNaN(Number(it.amount)) && it.date;

    // this function is used to validate the whole expense form
    const validate = (titleValue, currency, expenses) => {
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

    // this function is used to validate the expense row wihch is updated at index {index}
    const updateExpenseValidation = (index, updated, expenses) => {
        // get the updated expense row errors
        const rowErrors = getRowErrors(updated);

        // update the state (for the this row)
        setErrors(prev => {
            const next = { ...prev };   // see the current expense errors
            
            if (Object.keys(rowErrors).length === 0) {  // if the updated expense row has no errors, remove from the error state
                delete next[`expense_${index}`];
            } else {
                next[`expense_${index}`] = rowErrors;  // if the updated expense row has errors, update the error state
            }
            if (next.expenses && expenses.length > 0) delete next.expenses; // remove the overall expense errors (the ones applying to all of the expenses)
            return next;
        });
    };

    const clearFieldError = (fieldName) => {
        setErrors(prev => {
            const next = { ...prev };
            delete next[fieldName];
            return next;
        });
    };

    const setExpenseError = (message) => {
        setErrors(prev => ({ ...prev, expenses: message }));
    };

    const clearExpenseError = () => {
        setErrors(prev => {
            const next = { ...prev };
            delete next.expenses;
            return next;
        });
    };

    return {
        // State
        errors,
        submitError,
        
        // Setters
        setErrors,
        setSubmitError,
        
        // Validation functions
        getRowErrors,
        isExpenseValid,
        validate,
        clearFieldError,
        updateExpenseValidation,
        setExpenseError,
        clearExpenseError,
    };
};
