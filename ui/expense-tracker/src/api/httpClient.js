import axios from 'axios'

const tokenKey = 'auth_token';

const axiosClient = axios.create({
    baseURL: 'http://localhost:5000/api',
    headers: {
        'Content-Type': 'application/json'
    }
});

axiosClient.interceptors.request.use(config => {
    debugger;
    const token = localStorage.getItem(tokenKey)
        ?? sessionStorage.getItem(tokenKey);

    if (token) {
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
}, err => Promise.reject(err));

axiosClient.interceptors.response.use(response => response, err => {
    debugger;
    if (err.response?.status === 401) {

        if (window.location.pathname !== '/login') {
            window.location = '/login';
            return;
        }
    }

    return Promise.reject(err);
});

export { tokenKey };

export default axiosClient;