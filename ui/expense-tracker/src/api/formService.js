import httpClient from './httpClient'

const formService = {
    submitExpenseForm: async function(form) {
        const response = await httpClient.post('/form/submit', form);
        return response;
    }
}

export default formService;