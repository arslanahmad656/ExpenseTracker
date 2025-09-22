import axios from 'axios'

const tokenKey = 'auth_token';
const primaryRoleKey = 'role';

const axiosClient = axios.create({
    baseURL: 'http://localhost:5000/api',
    headers: {
        'Content-Type': 'application/json'
    }
});

axiosClient.interceptors.request.use(config => {
    const token = localStorage.getItem(tokenKey)
        ?? sessionStorage.getItem(tokenKey);

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
}, err => Promise.reject(err));

axiosClient.interceptors.response.use(response => response, err => {
    if (err.response?.status === 401) {

        if (window.location.pathname !== '/login') {
            window.location = '/login';
            return;
        }
    }

    return Promise.reject(err);
});

export { tokenKey, primaryRoleKey };

export { axiosClient as httpClient };

const httpClient = axiosClient;

export default httpClient;