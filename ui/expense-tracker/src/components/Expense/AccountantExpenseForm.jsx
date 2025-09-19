import React from 'react';
import { useParams } from 'react-router-dom';
import ExpenseFormBase from './ExpenseFormBase';

export default function AccountantExpenseForm({ formId: propFormId }) {
    const { formId: paramFormId } = useParams();
    const effectiveFormId = propFormId || paramFormId;

    return (
        <ExpenseFormBase
            mode="accountant"
            formId={effectiveFormId}
            title="Accountant Expense Review"
            submitButtonText=""
            canUpdateForm={true}
        />
    );
}
