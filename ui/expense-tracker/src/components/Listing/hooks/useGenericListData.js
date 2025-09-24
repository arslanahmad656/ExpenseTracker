import { useState, useEffect, useCallback } from 'react';
import { httpClient } from '../../../api/httpClient';

export const useGenericListData = (gridStructureUrl, fetchUrl, itemsPerPage) => {
    const [data, setData] = useState([]);
    const [columns, setColumns] = useState([]);
    const [loading, setLoading] = useState(false);
    const [structureLoading, setStructureLoading] = useState(true);
    const [error, setError] = useState('');
    const [totalItems, setTotalItems] = useState(0);

    // Fetch grid structure from API
    const fetchGridStructure = useCallback(async () => {
        setStructureLoading(true);
        setError('');
        
        try {
            const response = await httpClient.get(gridStructureUrl);
            setColumns(response.data || []);
        } catch (err) {
            setError(err.message || 'Failed to fetch grid structure');
            setColumns([]);
        } finally {
            setStructureLoading(false);
        }
    }, [gridStructureUrl]);

    // Fetch data from API
    const fetchData = useCallback(async (page = 1, sortField = '', sortDirection = 'asc', filters = {}) => {
        if (columns.length === 0) return; // Don't fetch data if structure not loaded
        
        setLoading(true);
        setError('');
        
        try {
            // Build query parameters
            const params = new URLSearchParams({
                pageNumber: page.toString(),
                itemsPerPage: itemsPerPage.toString(),
                ...(sortField && { orderBy: sortField }),
                ...(sortDirection && { sortOrder: sortDirection })
            });

            // Build filters array for request body
            const filtersArray = [];
            
            // Add filters
            Object.entries(filters).forEach(([key, value]) => {
                if (value && value !== '') {
                    filtersArray.push({ column: key, value: value });
                }
            });

            const url = `${fetchUrl}?${params.toString()}`;
            const response = await httpClient.post(url, filtersArray);
            
            setData(response.data.items || response.data || []);
            setTotalItems(response.data.totalCount || response.data.total || 0);
        } catch (err) {
            setError(err.message || 'Failed to fetch data');
            setData([]);
            setTotalItems(0);
        } finally {
            setLoading(false);
        }
    }, [columns.length, fetchUrl, itemsPerPage]);

    // Initial structure fetch
    useEffect(() => {
        if (gridStructureUrl) {
            fetchGridStructure();
        }
    }, [gridStructureUrl, fetchGridStructure]);

    // Fetch data after structure is loaded
    useEffect(() => {
        if (columns.length > 0 && fetchUrl) {
            fetchData(1, '', 'asc', {});
        }
    }, [columns, fetchUrl, itemsPerPage, fetchData]);

    return {
        // State
        data,
        columns,
        loading,
        structureLoading,
        error,
        totalItems,
        
        // Actions
        fetchData,
        fetchGridStructure,
    };
};
