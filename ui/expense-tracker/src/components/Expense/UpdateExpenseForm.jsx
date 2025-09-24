import React from 'react';
import ExpenseFormBase from './ExpenseFormBase';
import { FormStatus, ExpenseStatus } from '../../utils/enums';

export default function UpdateExpenseForm({ formId: propFormId }) {
    const canUpdateExpense = (expenseStatus) => 
        expenseStatus === ExpenseStatus.PendingApproval || expenseStatus === ExpenseStatus.Rejected;

    return (
        <ExpenseFormBase
            mode="update"
            formId={propFormId}
            title="Update Expense Form"
            submitButtonText="Update Form"
            canUpdateForm={true}
            canUpdateExpense={canUpdateExpense}
        />
    );
}