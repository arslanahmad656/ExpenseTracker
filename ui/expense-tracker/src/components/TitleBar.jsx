import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

const TitleBar = ({ user, onLogout }) => {
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

  return (
    <nav className="navbar navbar-expand-lg navbar-dark bg-primary shadow-sm">
      <div className="container-fluid">
        {/* Left side - App Title */}
        <div className="navbar-brand fw-bold fs-4">
          Expense Tracker App
        </div>

        {/* Right side - User Info and Dropdown */}
        <div className="navbar-nav ms-auto">
          <div className="nav-item dropdown">
            <button
              className="btn btn-link text-white text-decoration-none dropdown-toggle d-flex align-items-center"
              type="button"
              id="userDropdown"
              data-bs-toggle="dropdown"
              aria-expanded={isDropdownOpen}
              onClick={toggleDropdown}
            >
              <div className="text-end me-2">
                <div className="fw-semibold">
                  {user?.username || 'User'}
                </div>
                <small className="text-light opacity-75">
                  {user?.role || 'User'}
                </small>
              </div>
              <i className="bi bi-person-circle fs-4"></i>
            </button>

            <ul 
              className={`dropdown-menu dropdown-menu-end ${isDropdownOpen ? 'show' : ''}`}
              aria-labelledby="userDropdown"
            >
              <li>
                <button 
                  className="dropdown-item d-flex align-items-center"
                  onClick={handleViewExpenses}
                >
                  <i className="bi bi-list-ul me-2"></i>
                  View Expenses
                </button>
              </li>
              <li><hr className="dropdown-divider" /></li>
              <li>
                <button 
                  className="dropdown-item d-flex align-items-center text-danger"
                  onClick={handleLogout}
                >
                  <i className="bi bi-box-arrow-right me-2"></i>
                  Logout
                </button>
              </li>
            </ul>
          </div>
        </div>
      </div>

      {/* Bootstrap Icons CDN */}
      <link
        rel="stylesheet"
        href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.0/font/bootstrap-icons.css"
      />
    </nav>
  );
};

export default TitleBar;
