import React from 'react';
import { useParams } from 'react-router-dom';
import ExpenseFormBase from './ExpenseFormBase';
import { FormStatus, ExpenseStatus } from '../../utils/enums';

export default function ManagerExpenseForm({ formId: propFormId }) {
    const { formId: paramFormId } = useParams();
    const effectiveFormId = propFormId || paramFormId;
    
    const canUpdateExpense = (expenseStatus) => 
        expenseStatus === ExpenseStatus.PendingApproval;

    return (
        <ExpenseFormBase
            mode="manager"
            formId={effectiveFormId}
            title="Manager Expense Form Review"
            submitButtonText=""
            canUpdateForm={true}
            canUpdateExpense={canUpdateExpense}
        />
    );
}
