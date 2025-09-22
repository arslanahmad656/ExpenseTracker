import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import TitleBarItem from './TitleBarItem';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { logout } from '../../store/authSlice';

const TitleBar = ({ user, titleBarItems, onLogout }) => {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);

    const toggleDropdown = () => {
    setIsDropdownOpen(!isDropdownOpen);
    };

    const handleLogout = () => {
        setIsDropdownOpen(false);
        if (onLogout) {
            onLogout();
        }
    };

    const handleViewExpenses = () => {
    setIsDropdownOpen(false);
    // Navigate to View Expenses page
    // You can implement navigation logic here
    console.log('Navigate to View Expenses');
    };

    const hasItems = Array.isArray(titleBarItems) && titleBarItems.length > 0;

    return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary shadow-sm">
        <div className="container-fluid">
        
        <div className="navbar-brand fw-bold fs-4">
            Expense Tracker App
        </div>

        
        <div className="navbar-nav ms-auto">
            <div className={`nav-item ${hasItems ? 'dropdown' : ''}`}>
                <div className="d-flex align-items-center">
                    <div className="text-end me-2">
                        <div className="fw-semibold text-white">
                            {user?.username}
                        </div>
                        <small className="text-white-50">
                            {(user?.role ?? "").toUpperCase()}
                        </small>
                    </div>

                    {hasItems ? (
                        <>
                            <button
                                className="btn btn-link text-white text-decoration-none dropdown-toggle d-flex align-items-center"
                                type="button"
                                id="userDropdown"
                                data-bs-toggle="dropdown"
                                aria-expanded={isDropdownOpen}
                                onClick={toggleDropdown}
                            >
                                <i className="bi bi-person-circle fs-4 text-white"></i>
                            </button>

                            <ul 
                                className={`dropdown-menu dropdown-menu-end ${isDropdownOpen ? 'show' : ''}`}
                                aria-labelledby="userDropdown"
                            >
                                {
                                    titleBarItems.map(({ text, action, iconClass, type }, i) => (
                                        <li key={i}>
                                            <TitleBarItem text={text} action={action} iconClass={iconClass || type} />
                                        </li>
                                    ))
                                }
                            </ul>
                        </>
                    ) : (
                        <i className="bi bi-person-circle fs-4 text-white"></i>
                    )}
                </div>
            </div>
        </div>
        </div>
    </nav>
    );
};

export default TitleBar;
