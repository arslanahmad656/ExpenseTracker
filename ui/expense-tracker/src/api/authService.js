import { endPoints } from '../utils/endPoints';
import httpClient, { primaryRoleKey, tokenKey } from './httpClient'

const authService = {
    authenticate: async (username, password, persistToken) => {
        debugger;
        localStorage.removeItem(tokenKey);
        localStorage.removeItem(primaryRoleKey);
        sessionStorage.removeItem(tokenKey);
        sessionStorage.removeItem(primaryRoleKey);

        const response = await httpClient.post(endPoints.login(), {Username: username, Password: password});
        const token = response.data.token;

        if (token) {
            const store = persistToken ? localStorage : sessionStorage;
            store.setItem(tokenKey, token);
            store.setItem(primaryRoleKey, response.data.userInfo?.primaryRole);
            store.setItem('username', response.data.userInfo?.username);
        } else {
            throw 'Could not authenticate.';
        }

        return response.data;
    },

    clearSession: () => {
        //debugger;
        sessionStorage.clear();
        localStorage.clear();
    },

    getAuthenticatedRole: () => {
        const role = sessionStorage.getItem('role') ?? localStorage.getItem('role');
        return role;
    },

    getAuthenticatedUsername: () => {
        const username = sessionStorage.getItem('username') ?? localStorage.getItem('username');
        return username;
    },
}

export default authService;