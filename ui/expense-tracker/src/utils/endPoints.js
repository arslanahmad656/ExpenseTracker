export const endPoints = {
    login: () => '/auth/login',
    submitExpenseForm: () => '/form/submit',
    updateForm: (formId) => `/form/${formId}/update`,
    cancelForm: (formId) => `/form/${formId}/cancel`,
    cancelExpense: (expenseId) => `/form/expense/${expenseId}/cancel`,
    getDetailedForm: (formId) => `/form/${formId}/details`,
    approveForm: (formId) => `/form/${formId}/approve`,
    rejectForm: (formId) => `/form/${formId}/reject`,
    approveExpense: (expenseId) => `/form/expense/${expenseId}/approve`,
    rejectExpense: (expenseId) => `/form/expense/${expenseId}/reject`,
    reimburseForm: (formId) => `/form/${formId}/reimburse`,
    reimburseExpense: (expenseId) => `/form/expense/${expenseId}/reimburse`
}