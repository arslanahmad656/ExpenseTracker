import React from 'react';
import ExpenseFormBase from './ExpenseFormBase';

export default function ExpenseForm() {
    return (
        <ExpenseFormBase
            mode="create"
            title="Submit a new Expense Detail Form"
            submitButtonText="Submit"
            showCancelAllButton={false}
            canUpdateForm={true}
            canUpdateExpense={() => true}
        />
    );
}