import React from 'react';

// Custom hooks
import { 
    useGenericListData, 
    useGenericListFilters, 
    useGenericListPagination, 
    useGenericListSorting 
} from './hooks';

// UI Components
import { 
    GenericListFilters, 
    GenericListTable, 
    GenericListPagination, 
    GenericListLoadingStates, 
    GenericListInfo 
} from './components';

const GenericList = ({ 
    gridStructureUrl,
    fetchUrl, 
    itemsPerPage = 10,
    className = '',
    onItemClick = null,
    renderItem = null,
    onRenderCell = null,
    showSearch = true,
    showPagination = true,
    showSorting = true
}) => {
    // Custom hooks
    const {
        data,
        columns,
        loading,
        structureLoading,
        error,
        totalItems,
        fetchData,
    } = useGenericListData(gridStructureUrl, fetchUrl, itemsPerPage);

    const {
        filters,
        tempFilters,
        handleFilterInputChange,
        applyFilters,
    } = useGenericListFilters(columns);

    const {
        currentPage,
        totalPages,
        handlePageChange,
        resetToFirstPage,
    } = useGenericListPagination(itemsPerPage, totalItems);

    const {
        sortField,
        sortDirection,
        handleSort,
        getSortIcon,
    } = useGenericListSorting();

    // Coordination functions
    const handleApplyFilters = () => {
        applyFilters((appliedFilters) => {
            resetToFirstPage();
            fetchData(1, sortField, sortDirection, appliedFilters);
        });
    };

    const handleSortWithFetch = (field) => {
        handleSort(field, columns, (newField, newDirection) => {
            resetToFirstPage();
            fetchData(1, newField, newDirection, filters);
        });
    };

    const handlePageChangeWithFetch = (page) => {
        handlePageChange(page, (newPage) => {
            fetchData(newPage, sortField, sortDirection, filters);
        });
    };


    // Check for loading/error states first
    const loadingState = GenericListLoadingStates({
        structureLoading,
        loading,
        data,
        error
    });

    if (loadingState) {
        return loadingState;
    }

    return (
        <div className={`generic-list ${className}`}>
            {/* Filters */}
            {showSearch && (
                <GenericListFilters
                    columns={columns}
                    tempFilters={tempFilters}
                    onFilterInputChange={handleFilterInputChange}
                    onApplyFilters={handleApplyFilters}
                    loading={loading}
                />
            )}

            {/* Table */}
            <GenericListTable
                columns={columns}
                data={data}
                onSort={handleSortWithFetch}
                onItemClick={onItemClick}
                renderItem={renderItem}
                onRenderCell={onRenderCell}
                showSorting={showSorting}
                getSortIcon={getSortIcon}
            />

            {/* Pagination */}
            {showPagination && (
                <GenericListPagination
                    currentPage={currentPage}
                    totalPages={totalPages}
                    onPageChange={handlePageChangeWithFetch}
                    loading={loading}
                />
            )}

            {/* Info */}
            <GenericListInfo
                showPagination={showPagination}
                totalItems={totalItems}
                currentPage={currentPage}
                itemsPerPage={itemsPerPage}
            />
        </div>
    );
};

export default GenericList;
