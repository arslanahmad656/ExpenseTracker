import React from 'react';

export const GenericListPagination = ({
    currentPage,
    totalPages,
    onPageChange,
    loading
}) => {
    if (totalPages <= 1) return null;

    const pages = [];
    const maxVisiblePages = 5;
    let startPage = Math.max(1, currentPage - Math.floor(maxVisiblePages / 2));
    let endPage = Math.min(totalPages, startPage + maxVisiblePages - 1);

    if (endPage - startPage + 1 < maxVisiblePages) {
        startPage = Math.max(1, endPage - maxVisiblePages + 1);
    }

    for (let i = startPage; i <= endPage; i++) {
        pages.push(
            <li key={i} className={`page-item ${i === currentPage ? 'active' : ''}`}>
                <button 
                    className="page-link" 
                    onClick={() => onPageChange(i)}
                    disabled={loading}
                >
                    {i}
                </button>
            </li>
        );
    }

    return (
        <nav aria-label="Pagination">
            <ul className="pagination justify-content-center">
                <li className={`page-item ${currentPage === 1 ? 'disabled' : ''}`}>
                    <button 
                        className="page-link" 
                        onClick={() => onPageChange(currentPage - 1)}
                        disabled={currentPage === 1 || loading}
                    >
                        <i className="bi bi-chevron-left"></i>
                    </button>
                </li>
                {pages}
                <li className={`page-item ${currentPage === totalPages ? 'disabled' : ''}`}>
                    <button 
                        className="page-link" 
                        onClick={() => onPageChange(currentPage + 1)}
                        disabled={currentPage === totalPages || loading}
                    >
                        <i className="bi bi-chevron-right"></i>
                    </button>
                </li>
            </ul>
        </nav>
    );
};
