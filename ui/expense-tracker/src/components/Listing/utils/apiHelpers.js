// Build query parameters for API calls
export const buildApiParams = (page, itemsPerPage, sortField, sortDirection) => {
    const params = new URLSearchParams({
        pageNumber: page.toString(),
        itemsPerPage: itemsPerPage.toString(),
        ...(sortField && { orderBy: sortField }),
        ...(sortDirection && { sortOrder: sortDirection })
    });
    return params;
};

// Build filters array for request body
export const buildFiltersArray = (filters) => {
    const filtersArray = [];
    
    Object.entries(filters).forEach(([key, value]) => {
        if (value && value !== '') {
            filtersArray.push({ column: key, value: value });
        }
    });
    
    return filtersArray;
};
