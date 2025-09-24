import React from 'react';

export const GenericListTable = ({
    columns,
    data,
    onSort,
    onItemClick,
    renderItem,
    onRenderCell,
    showSorting,
    getSortIcon
}) => {
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

    return (
        <div className="table-responsive">
            <table className="table table-hover">
                <thead className="table-light">
                    <tr>
                        {columns.map((column, index) => (
                            <th 
                                key={column.key}
                                className={showSorting && column.isSortable ? 'cursor-pointer' : ''}
                                onClick={() => showSorting && column.isSortable && onSort(column.key)}
                            >
                                <div className="d-flex align-items-center justify-content-between">
                                    <span>{column.displayName}</span>
                                    {showSorting && column.isSortable && getSortIcon(column.key)}
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
    );
};
