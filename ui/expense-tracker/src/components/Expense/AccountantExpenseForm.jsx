import React from 'react';
import { useParams } from 'react-router-dom';
import ExpenseFormBase from './ExpenseFormBase';
import { FormStatus, ExpenseStatus } from '../../utils/enums';

export default function AccountantExpenseForm({ formId: propFormId }) {
    const { formId: paramFormId } = useParams();
    const effectiveFormId = propFormId || paramFormId;
    
    const canUpdateExpense = (expenseStatus) => 
        expenseStatus !== ExpenseStatus.Cancelled && expenseStatus !== ExpenseStatus.Reimbursed;

    return (
        <ExpenseFormBase
            mode="accountant"
            formId={effectiveFormId}
            title="Accountant Expense Review"
            submitButtonText=""
            canUpdateForm={true}
            canUpdateExpense={canUpdateExpense}
        />
    );
}
