import httpClient, { tokenKey } from './httpClient'

const authService = {
    authenticate: async (username, password, persistToken) => {
        debugger;
        const response = await httpClient.post('/auth/login', {Username: username, Password: password});
        const token = response.data.token;

        debugger;
        if (token) {
            const store = persistToken ? localStorage : sessionStorage;
            store.setItem(tokenKey, token);
        } else {
            throw 'Could not authenticate.';
        }

        return response.data;
    },

    clearSession: () => {
        debugger;
        sessionStorage.clear();
        localStorage.clear();
    }
}

export default authService;