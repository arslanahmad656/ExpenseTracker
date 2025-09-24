import { useState } from 'react';

export const useGenericListPagination = (itemsPerPage, totalItems) => {
    const [currentPage, setCurrentPage] = useState(1);

    // Calculate total pages
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    // Handle page change
    const handlePageChange = (page, onPageChange) => {
        setCurrentPage(page);
        onPageChange(page);
    };

    // Reset to first page
    const resetToFirstPage = () => {
        setCurrentPage(1);
    };

    // Get pagination info
    const getPaginationInfo = () => {
        const startItem = ((currentPage - 1) * itemsPerPage) + 1;
        const endItem = Math.min(currentPage * itemsPerPage, totalItems);
        return { startItem, endItem, totalItems };
    };

    return {
        // State
        currentPage,
        totalPages,
        
        // Actions
        handlePageChange,
        resetToFirstPage,
        setCurrentPage,
        
        // Utils
        getPaginationInfo,
    };
};
