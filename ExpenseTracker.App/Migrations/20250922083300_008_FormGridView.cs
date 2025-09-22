using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

// manual migration 

namespace ExpenseTracker.App.Migrations
{
    /// <inheritdoc />
    public partial class _008_FormGridView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                CREATE OR ALTER VIEW vw_FormGrid AS
                SELECT f.Id [FormId], f.TrackingId, f.Title, f.[Status], fc.Amount, c.Code [CurrencyCode] FROM Form f
                LEFT OUTER JOIN
                	(
                		SELECT FormId, SUM(Amount) Amount FROM Expense
                		GROUP BY FormId
                	) fc on f.Id = fc.FormId
                LEFT OUTER JOIN Currency c ON f.CurrencyId = c.Id;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW vw_FormGrid;");
        }
    }
}
