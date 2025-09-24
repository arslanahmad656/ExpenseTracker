import React from 'react';

export const GenericListFilters = ({
    columns,
    tempFilters,
    onFilterInputChange,
    onApplyFilters,
    loading
}) => {
    const searchableColumns = columns.filter(column => column.isSearchable).slice(0, 3);

    if (searchableColumns.length === 0) return null;

    return (
        <div className="row mb-3">
            <div className="col-md-10">
                <div className="d-flex gap-2">
                    {searchableColumns.map((column) => (
                        <div key={column.key} className="flex-grow-1">
                            <input
                                type="text"
                                className="form-control form-control-sm"
                                placeholder={`Filter by ${column.displayName}`}
                                value={tempFilters[column.key] || ''}
                                onChange={(e) => onFilterInputChange(column.key, e.target.value)}
                                disabled={loading}
                            />
                        </div>
                    ))}
                </div>
            </div>
            <div className="col-md-2">
                <button
                    className="btn btn-primary btn-sm w-100"
                    onClick={onApplyFilters}
                    disabled={loading}
                >
                    <i className="bi bi-funnel me-1"></i>
                    Apply Filter
                </button>
            </div>
        </div>
    );
};
