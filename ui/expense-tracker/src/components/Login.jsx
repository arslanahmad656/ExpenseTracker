import React, { useState } from 'react';
import authService from '../api/authService';
import { useDispatch } from 'react-redux';
import { login } from '../store/authSlice';

const Login = () => {
    const dispath = useDispatch();

    const [formData, setFormData] = useState({
    username: '',
    password: '',
    rememberMe: false
    });

    const [errors, setErrors] = useState({});
    const [authError, setAuthError] = useState('');

    const handleChange = (e) => {
    const { name, value, type, checked } = e.target;
    setFormData(prev => ({
        ...prev,
        [name]: type === 'checkbox' ? checked : value
    }));

    if (errors[name]) {
        setErrors(prev => ({
        ...prev,
        [name]: ''
        }));
    }

    if (authError) {
        setAuthError('');
    }
    };

    const validateForm = () => {
    debugger;
    const newErrors = {};

    if (!formData.username.trim()) {
        newErrors.username = 'Username is required';
    }

    if (!formData.password) {
        newErrors.password = 'Password is required';
    } else if (formData.password.length < 6) {
        newErrors.password = 'Password must be at least 6 characters';
    }

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e) => {
    e.preventDefault();

    setAuthError('');

    if (validateForm()) {
        try {
            const response = await authService.authenticate(formData.username, formData.password, formData.rememberMe);

            debugger;
            const { username, primaryRole: role} = response.userInfo;
            dispath(login({username, role}));
        }
        catch (err) {
            setAuthError(err.message || 'Authentication failed. Please check your credentials.');
        }
    }
    };

    return (
    <div className="min-vh-100 d-flex align-items-center justify-content-center bg-light">
        <div className="container">
        <div className="row justify-content-center">
            <div className="col-md-6 col-lg-4">
            <div className="card shadow-lg border-0">
                <div className="card-body p-5">
                    
                    <div className="text-center mb-4">
                    <h2 className="fw-bold text-success mb-2">Welcome to Expense Tracker</h2>
                    <p className="text-muted">Sign in to your account</p>
                    </div>

                    {authError && (
                    <div className="alert alert-danger mb-4" role="alert">
                        <i className="bi bi-exclamation-triangle-fill me-2"></i>
                        {authError}
                    </div>
                    )}

                <form onSubmit={handleSubmit} noValidate>
                    <div className="mb-3">
                    <label htmlFor="username" className="form-label fw-semibold">
                        Username
                    </label>
                    <div className="input-group">
                        <span className="input-group-text bg-light border-end-0">
                        <i className="bi bi-person-fill text-muted"></i>
                        </span>
                        <input
                        type="text"
                        className={`form-control border-start-0 ${errors.username ? 'is-invalid' : ''}`}
                        id="username"
                        name="username"
                        value={formData.username}
                        onChange={handleChange}
                        placeholder="Enter your username"
                        autoComplete="username"
                        />
                    </div>
                    {errors.username && (
                        <div className="invalid-feedback d-block">
                        {errors.username}
                        </div>
                    )}
                    </div>

                    <div className="mb-4">
                    <label htmlFor="password" className="form-label fw-semibold">
                        Password
                    </label>
                    <div className="input-group">
                        <span className="input-group-text bg-light border-end-0">
                        <i className="bi bi-lock-fill text-muted"></i>
                        </span>
                        <input
                        type="password"
                        className={`form-control border-start-0 ${errors.password ? 'is-invalid' : ''}`}
                        id="password"
                        name="password"
                        value={formData.password}
                        onChange={handleChange}
                        placeholder="Enter your password"
                        autoComplete="current-password"
                        />
                    </div>
                    {errors.password && (
                        <div className="invalid-feedback d-block">
                        {errors.password}
                        </div>
                    )}
                    </div>

                    <div className="d-flex justify-content-between align-items-center mb-4">
                        <div className="form-check">
                        <input
                            className="form-check-input"
                            type="checkbox"
                            id="rememberMe"
                            name="rememberMe"
                            checked={formData.rememberMe}
                            onChange={handleChange}
                        />
                        <label className="form-check-label text-muted" htmlFor="rememberMe">
                            Remember me
                        </label>
                        </div>
                    <a href="#" className="text-decoration-none text-primary small">
                        Forgot password?
                    </a>
                    </div>

                    <button
                    type="submit"
                    className="btn btn-primary w-100 py-2 fw-semibold"
                    >
                    Sign In
                    </button>
                </form>

                {/* <div className="text-center mt-4">
                    <p className="text-muted small mb-0">
                    Don't have an account?{' '}
                    <a href="#" className="text-decoration-none fw-semibold">
                        Sign up here
                    </a>
                    </p>
                </div> */}
                </div>
            </div>
            </div>
        </div>
        </div>
    </div>
    );
};

export default Login;
