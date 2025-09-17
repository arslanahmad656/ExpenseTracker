using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.App.Migrations
{
    /// <inheritdoc />
    public partial class _007_HistoryMetadata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RejectionReason",
                table: "ExpenseHistory",
                newName: "Note");

            migrationBuilder.AddColumn<string>(
                name: "CurrentState",
                table: "FormHistory",
                type: "nvarchar(2047)",
                maxLength: 2047,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "FormHistory",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousState",
                table: "FormHistory",
                type: "nvarchar(2047)",
                maxLength: 2047,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentState",
                table: "ExpenseHistory",
                type: "nvarchar(2047)",
                maxLength: 2047,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousState",
                table: "ExpenseHistory",
                type: "nvarchar(2047)",
                maxLength: 2047,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "FormHistory");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "FormHistory");

            migrationBuilder.DropColumn(
                name: "PreviousState",
                table: "FormHistory");

            migrationBuilder.DropColumn(
                name: "CurrentState",
                table: "ExpenseHistory");

            migrationBuilder.DropColumn(
                name: "PreviousState",
                table: "ExpenseHistory");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "ExpenseHistory",
                newName: "RejectionReason");
        }
    }
}
