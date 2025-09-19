import { endPoints } from '../utils/endPoints';
import httpClient from './httpClient'

const formService = {
    submitExpenseForm: async function(form) {
        const response = await httpClient.post(endPoints.submitExpenseForm(), form);
        return response;
    },

    updateForm: async function(formId, form) {
        const response = await httpClient.put(endPoints.updateForm(formId), form);
        return response;
    },

    cancelForm: async function(formId, reason) {
        const response = await httpClient.post(endPoints.cancelForm(formId), { reason });
        return response;
    },

    cancelExpense: async function(expenseId, reason) {
        debugger;
        const response = await httpClient.post(endPoints.cancelExpense(expenseId), { reason });
        return response;
    },

    getDetailedForm: async (formId) => {
        debugger;
        const response = await httpClient.get(endPoints.getDetailedForm(formId));
        debugger;
        console.log(response);
        return response.data;
    },

    approveForm: async function(formId) {
        const response = await httpClient.post(endPoints.approveForm(formId));
        return response;
    },

    rejectForm: async function(formId, reason) {
        const response = await httpClient.post(endPoints.rejectForm(formId), { reason });
        return response;
    },


    reimburseForm: async function(formId) {
        const response = await httpClient.post(endPoints.reimburseForm(formId));
        return response;
    },

}

export default formService;