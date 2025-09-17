export const endPoints = {
    login: () => '/auth/login',
    submitExpenseForm: () => '/form/submit',
    updateForm: (formId) => `/form/${formId}/update`,
    cancelForm: (formId) => `/form/${formId}/cancel`,
    cancelExpense: (expenseId) => `/expense/${expenseId}/cancel`,
    getDetailedForm: (formId) => `/form/${formId}/details`
}