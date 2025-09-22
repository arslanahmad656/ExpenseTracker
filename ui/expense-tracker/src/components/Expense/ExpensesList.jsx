import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { GenericList } from '../Listing';
import { FormHistoryModal } from '../FormHistory';
import { endPoints } from '../../utils/endPoints';
import { FormStatus } from '../../utils/enums';

const ExpensesList = () => {
    const navigate = useNavigate();
    const [showHistoryModal, setShowHistoryModal] = useState(false);
    const [selectedFormId, setSelectedFormId] = useState(null);

    const handleExpenseClick = (expense) => {
        // Navigate to expense details page
        // navigate(`/form/${expense.id}/details`);
    };

    const handleCreateExpense = () => {
        // Navigate to create expense form
        navigate('/form/create');
    };

    const handleShowHistory = (formId) => {
        setSelectedFormId(formId);
        setShowHistoryModal(true);
    };

    const handleCloseHistory = () => {
        setShowHistoryModal(false);
        setSelectedFormId(null);
    };

    const role = sessionStorage.getItem('role') ?? localStorage.getItem('role');
    const location = useLocation();

    // Custom cell renderer for different column types
    const renderCell = (item, column, value) => {
        if (column.key === 'actions') {
            // debugger;
        }
        const formId = item.formId;
        console.log(formId);
        switch (column.key) {
            case 'status':
                return (
                    <span className={`badge ${
                        value === FormStatus.PendingApproval ? 'bg-warning' :
                        value === FormStatus.PendingReimbursement ? 'bg-success' :
                        value === FormStatus.Rejected ? 'bg-danger' :
                        value === FormStatus.Reimbursed ? 'bg-info' :
                        value === FormStatus.Cancelled ? 'bg-secondary' :
                        'bg-light text-dark'
                    }`}>
                        {FormStatus[value]}
                    </span>
                );
            
            case 'amount':
                return (
                    <span className="fw-semibold">
                        ${parseFloat(value || 0).toFixed(2)}
                    </span>
                );
            
            case 'actions':
                return (
                    <div className="d-flex gap-2">
                        {role === 'Employee' && (
                            <>
                                <button 
                                    className="btn btn-outline-primary btn-sm"
                                    onClick={() => {
                                        navigate(`/form/${formId}/details`);
                                        // window.location.pathname = `/form/${formId}/details`;
                                    }}
                                >
                                    Details
                                </button>
                                <button 
                                    className="btn btn-outline-secondary btn-sm"
                                    onClick={() => {
                                        navigate(`/form/${formId}/edit`);
                                    }}
                                >
                                    Edit
                                </button>
                            </>
                        )}
                        {(role === 'Manager' || role === 'Accountant') && (
                            <button 
                                className="btn btn-outline-primary btn-sm"
                                onClick={() => {
                                    const postFix = role === 'Manager' ? 'managerial' : 'accountant';
                                    navigate(`/form/${formId}/${postFix}`);
                                }}
                            >
                                Details
                            </button>
                        )}
                        {
                            (role === 'Administrator') && (
                                <button 
                                    className="btn btn-outline-primary btn-sm"
                                    onClick={() => {
                                        handleShowHistory(formId);
                                    }}
                                >
                                    History
                                </button>
                            )
                        }
                    </div>
                );
            
            default:
                return value;
        }
    };

    return (
        <div className="container-fluid">
            <div className="row">
                <div className="col-12">
                    <div className="d-flex justify-content-between align-items-center mb-4">
                        <div>
                            <h2 className="mb-1">Expense Forms</h2>
                            <p className="text-muted mb-0">Manage and review expense forms</p>
                        </div>
                        <button 
                            className="btn btn-primary"
                            onClick={handleCreateExpense}
                        >
                            <i className="bi bi-plus-circle me-2"></i>
                            Create New Expense
                        </button>
                    </div>

                    <div className="card shadow-sm border-0">
                        <div className="card-body p-0">
                            <GenericList
                                gridStructureUrl={endPoints.formGridStructure()}
                                fetchUrl={endPoints.formGridSearch()}
                                itemsPerPage={10}
                                onItemClick={handleExpenseClick}
                                onRenderCell={renderCell}
                                searchPlaceholder="Search expense forms..."
                                className="expenses-list"
                                showSearch={true}
                                showPagination={true}
                                showSorting={true}
                            />
                        </div>
                    </div>
                </div>
            </div>

            {/* Form History Modal */}
            <FormHistoryModal
                formId={selectedFormId}
                isOpen={showHistoryModal}
                onClose={handleCloseHistory}
            />
        </div>
    );
};

export default ExpensesList;
