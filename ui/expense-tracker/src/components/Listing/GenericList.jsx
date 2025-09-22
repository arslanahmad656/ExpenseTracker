import React, { useState, useEffect, useMemo } from 'react';
import { httpClient } from '../../api/httpClient';

const GenericList = ({ 
    gridStructureUrl,
    fetchUrl, 
    itemsPerPage = 10,
    className = '',
    onItemClick = null,
    renderItem = null,
    onRenderCell = null,
    searchPlaceholder = "Search...",
    showSearch = true,
    showPagination = true,
    showSorting = true
}) => {
    const [data, setData] = useState([]);
    const [columns, setColumns] = useState([]);
    const [loading, setLoading] = useState(false);
    const [structureLoading, setStructureLoading] = useState(true);
    const [error, setError] = useState('');
    const [currentPage, setCurrentPage] = useState(1);
    const [totalItems, setTotalItems] = useState(0);
    const [sortField, setSortField] = useState('');
    const [sortDirection, setSortDirection] = useState('asc');
    const [filters, setFilters] = useState({});
    const [tempFilters, setTempFilters] = useState({});

    // Calculate total pages
    const totalPages = Math.ceil(totalItems / itemsPerPage);

    // Get column keys for API calls
    const columnKeys = useMemo(() => {
        return columns.map(col => col.key);
    }, [columns]);

    // Fetch grid structure from API
    const fetchGridStructure = async () => {
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
    };

    // Fetch data from API
    const fetchData = async (page = 1, sortField = '', sortDirection = 'asc', filters = {}) => {
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
    };

    // Initial structure fetch
    useEffect(() => {
        if (gridStructureUrl) {
            fetchGridStructure();
        }
    }, [gridStructureUrl]);

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

    // Fetch data after structure is loaded
    useEffect(() => {
        if (columns.length > 0 && fetchUrl) {
            fetchData(currentPage, sortField, sortDirection, filters);
        }
    }, [columns, fetchUrl, itemsPerPage]);

    // Handle filter input change (temporary)
    const handleFilterInputChange = (field, value) => {
        setTempFilters(prev => ({
            ...prev,
            [field]: value
        }));
    };

    // Apply filters
    const applyFilters = () => {
        setFilters(tempFilters);
        setCurrentPage(1);
        fetchData(1, sortField, sortDirection, tempFilters);
    };

    // Handle sorting (immediate application)
    const handleSort = (field) => {
        // Check if the field is sortable
        const column = columns.find(col => col.key === field);
        if (!column || !column.isSortable) return;
        
        const newDirection = sortField === field && sortDirection === 'asc' ? 'desc' : 'asc';
        setSortField(field);
        setSortDirection(newDirection);
        setCurrentPage(1);
        fetchData(1, field, newDirection, filters);
    };

    // Handle pagination
    const handlePageChange = (page) => {
        setCurrentPage(page);
        fetchData(page, sortField, sortDirection, filters);
    };


    // Render sort icon
    const renderSortIcon = (field) => {
        if (sortField !== field) {
            return <i className="bi bi-arrow-down-up text-muted"></i>;
        }
        return sortDirection === 'asc' 
            ? <i className="bi bi-arrow-up text-primary"></i>
            : <i className="bi bi-arrow-down text-primary"></i>;
    };

    // Render custom cell content
    const renderCellContent = (item, column, value) => {
        let customValue; 
        if (onRenderCell) {
            customValue = onRenderCell(item, column, value);
        }
        return customValue ?? value;
    };

    // Render custom item or default
    const renderListItem = (item, index) => {
        if (renderItem) {
            return renderItem(item, index);
        }

        return (
            <tr 
                key={index} 
                className={onItemClick ? 'cursor-pointer' : ''}
                onClick={() => onItemClick && onItemClick(item)}
            >
                {columns.map((column, colIndex) => (
                    <td key={column.key} className="align-middle">
                        {renderCellContent(item, column, item[column.key])}
                    </td>
                ))}
            </tr>
        );
    };

    // Render pagination
    const renderPagination = () => {
        if (!showPagination || totalPages <= 1) return null;

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
                        onClick={() => handlePageChange(i)}
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
                            onClick={() => handlePageChange(currentPage - 1)}
                            disabled={currentPage === 1 || loading}
                        >
                            <i className="bi bi-chevron-left"></i>
                        </button>
                    </li>
                    {pages}
                    <li className={`page-item ${currentPage === totalPages ? 'disabled' : ''}`}>
                        <button 
                            className="page-link" 
                            onClick={() => handlePageChange(currentPage + 1)}
                            disabled={currentPage === totalPages || loading}
                        >
                            <i className="bi bi-chevron-right"></i>
                        </button>
                    </li>
                </ul>
            </nav>
        );
    };

    if (structureLoading) {
        return (
            <div className="d-flex justify-content-center align-items-center" style={{ minHeight: '200px' }}>
                <div className="text-center">
                    <div className="spinner-border text-primary mb-2" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </div>
                    <div className="text-muted">Loading grid structure...</div>
                </div>
            </div>
        );
    }

    if (loading && data.length === 0) {
        return (
            <div className="d-flex justify-content-center align-items-center" style={{ minHeight: '200px' }}>
                <div className="text-center">
                    <div className="spinner-border text-primary mb-2" role="status">
                        <span className="visually-hidden">Loading...</span>
                    </div>
                    <div className="text-muted">Loading data...</div>
                </div>
            </div>
        );
    }

    return (
        <div className={`generic-list ${className}`}>
            {/* Filters */}
            {showSearch && (
                <div className="row mb-3">
                    <div className="col-md-10">
                        <div className="d-flex gap-2">
                            {columns
                                .filter(column => column.isSearchable)
                                .slice(0, 3)
                                .map((column) => (
                                <div key={column.key} className="flex-grow-1">
                                    <input
                                        type="text"
                                        className="form-control form-control-sm"
                                        placeholder={`Filter by ${column.displayName}`}
                                        value={tempFilters[column.key] || ''}
                                        onChange={(e) => handleFilterInputChange(column.key, e.target.value)}
                                        disabled={loading}
                                    />
                                </div>
                            ))}
                        </div>
                    </div>
                    <div className="col-md-2">
                        <button
                            className="btn btn-primary btn-sm w-100"
                            onClick={applyFilters}
                            disabled={loading}
                        >
                            <i className="bi bi-funnel me-1"></i>
                            Apply Filter
                        </button>
                    </div>
                </div>
            )}

            {/* Error Message */}
            {error && (
                <div className="alert alert-danger" role="alert">
                    <i className="bi bi-exclamation-triangle me-2"></i>
                    {error}
                </div>
            )}

            {/* Table */}
            <div className="table-responsive">
                <table className="table table-hover">
                    <thead className="table-light">
                        <tr>
                            {columns.map((column, index) => (
                                <th 
                                    key={column.key}
                                    className={showSorting && column.isSortable ? 'cursor-pointer' : ''}
                                    onClick={() => showSorting && column.isSortable && handleSort(column.key)}
                                >
                                    <div className="d-flex align-items-center justify-content-between">
                                        <span>{column.displayName}</span>
                                        {showSorting && column.isSortable && renderSortIcon(column.key)}
                                    </div>
                                </th>
                            ))}
                        </tr>
                    </thead>
                    <tbody>
                        {data.length === 0 ? (
                            <tr>
                                <td colSpan={columns.length} className="text-center text-muted py-4">
                                    <i className="bi bi-inbox me-2"></i>
                                    No data found
                                </td>
                            </tr>
                        ) : (
                            data.map((item, index) => renderListItem(item, index))
                        )}
                    </tbody>
                </table>
            </div>

            {/* Pagination */}
            {renderPagination()}

            {/* Info */}
            {showPagination && totalItems > 0 && (
                <div className="text-muted small text-center mt-2">
                    Showing {((currentPage - 1) * itemsPerPage) + 1} to {Math.min(currentPage * itemsPerPage, totalItems)} of {totalItems} entries
                </div>
            )}
        </div>
    );
};

export default GenericList;
