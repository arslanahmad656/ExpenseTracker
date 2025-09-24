import React from 'react';

export const GenericListInfo = ({
    showPagination,
    totalItems,
    currentPage,
    itemsPerPage
}) => {
    if (!showPagination || totalItems === 0) return null;

    const startItem = ((currentPage - 1) * itemsPerPage) + 1;
    const endItem = Math.min(currentPage * itemsPerPage, totalItems);

    return (
        <div className="text-muted small text-center mt-2">
            Showing {startItem} to {endItem} of {totalItems} entries
        </div>
    );
};
