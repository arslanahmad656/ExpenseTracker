import httpClient from './httpClient'

const formService = {
    submitExpenseForm: async function(form) {
        const response = await httpClient.post('/form/submit', form);
        return response;
    },

    getDetailedForm: async (formId) => {
        debugger;
        const response = await httpClient.get(`/form/${formId}/details`);
        debugger;
        console.log(response);
        return response.data;
    }
}

export default formService;