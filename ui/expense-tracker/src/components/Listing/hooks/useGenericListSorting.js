import { useState } from 'react';

export const useGenericListSorting = () => {
    const [sortField, setSortField] = useState('');
    const [sortDirection, setSortDirection] = useState('asc');

    // Handle sorting
    const handleSort = (field, columns, onSort) => {
        // Check if the field is sortable
        const column = columns.find(col => col.key === field);
        if (!column || !column.isSortable) return;
        
        const newDirection = sortField === field && sortDirection === 'asc' ? 'desc' : 'asc';
        setSortField(field);
        setSortDirection(newDirection);
        onSort(field, newDirection);
    };

    // Reset sorting
    const resetSorting = () => {
        setSortField('');
        setSortDirection('asc');
    };

    // Get sort icon for a field
    const getSortIcon = (field) => {
        if (sortField !== field) {
            return <i className="bi bi-arrow-down-up text-muted"></i>;
        }
        return sortDirection === 'asc' 
            ? <i className="bi bi-arrow-up text-primary"></i>
            : <i className="bi bi-arrow-down text-primary"></i>;
    };

    return {
        // State
        sortField,
        sortDirection,
        
        // Actions
        handleSort,
        resetSorting,
        
        // Utils
        getSortIcon,
    };
};
