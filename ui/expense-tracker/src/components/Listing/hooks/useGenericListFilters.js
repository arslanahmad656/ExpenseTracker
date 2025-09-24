import { useState, useEffect } from 'react';

export const useGenericListFilters = (columns) => {
    const [filters, setFilters] = useState({});
    const [tempFilters, setTempFilters] = useState({});

    // Initialize tempFilters when columns are loaded
    useEffect(() => {
        if (columns.length > 0) {
            const initialTempFilters = {};
            columns
                .filter(column => column.isSearchable)
                .forEach(column => {
                    initialTempFilters[column.key] = '';
                });
            setTempFilters(initialTempFilters);
        }
    }, [columns]);

    // Handle filter input change (temporary)
    const handleFilterInputChange = (field, value) => {
        setTempFilters(prev => ({
            ...prev,
            [field]: value
        }));
    };

    // Apply filters
    const applyFilters = (onApply) => {
        setFilters(tempFilters);
        onApply(tempFilters);
    };

    // Clear filters
    const clearFilters = () => {
        const clearedTempFilters = {};
        Object.keys(tempFilters).forEach(key => {
            clearedTempFilters[key] = '';
        });
        setTempFilters(clearedTempFilters);
        setFilters({});
    };

    return {
        // State
        filters,
        tempFilters,
        
        // Actions
        handleFilterInputChange,
        applyFilters,
        clearFilters,
        setFilters,
    };
};
