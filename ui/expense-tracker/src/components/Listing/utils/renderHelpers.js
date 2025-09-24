// Render sort icon for table headers
export const renderSortIcon = (field, sortField, sortDirection) => {
    if (sortField !== field) {
        return <i className="bi bi-arrow-down-up text-muted"></i>;
    }
    return sortDirection === 'asc' 
        ? <i className="bi bi-arrow-up text-primary"></i>
        : <i className="bi bi-arrow-down text-primary"></i>;
};

// Render cell content with custom renderer support
export const renderCellContent = (item, column, value, onRenderCell) => {
    let customValue; 
    if (onRenderCell) {
        customValue = onRenderCell(item, column, value);
    }
    return customValue ?? value;
};
