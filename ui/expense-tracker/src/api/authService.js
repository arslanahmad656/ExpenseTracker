import { endPoints } from '../utils/endPoints';
import httpClient, { tokenKey } from './httpClient'

const authService = {
    authenticate: async (username, password, persistToken) => {
        const response = await httpClient.post(endPoints.login(), {Username: username, Password: password});
        const token = response.data.token;

        if (token) {
            const store = persistToken ? localStorage : sessionStorage;
            store.setItem(tokenKey, token);
        } else {
            throw 'Could not authenticate.';
        }

        return response.data;
    },

    clearSession: () => {
        //debugger;
        sessionStorage.clear();
        localStorage.clear();
    }
}

export default authService;