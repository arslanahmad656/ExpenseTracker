using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTracker.App.Migrations
{
    /// <inheritdoc />
    public partial class _005_MiscEnhancements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_ExpenseState_ExpenseStateId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Form_FormState_FormStateId",
                table: "Form");

            migrationBuilder.DropTable(
                name: "ExpenseState");

            migrationBuilder.DropTable(
                name: "FormState");

            migrationBuilder.DropIndex(
                name: "IX_Form_FormStateId",
                table: "Form");

            migrationBuilder.DropIndex(
                name: "IX_Expense_ExpenseStateId",
                table: "Expense");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "FormHistory",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "FormStateId",
                table: "Form",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "ExpenseHistory",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "ExpenseStateId",
                table: "Expense",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "TrackingId",
                table: "Form",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<string>(
                name: "TrackingId",
                table: "Expense",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Expense_Amount_GreaterThanZero",
                table: "Expense",
                sql: "[AMOUNT] > 0");

            migrationBuilder.CreateIndex(
                name: "IX_Currency_Code",
                table: "Currency",
                column: "Code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Expense_Amount_GreaterThanZero",
                table: "Expense");

            migrationBuilder.DropIndex(
                name: "IX_Currency_Code",
                table: "Currency");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "FormHistory",
                newName: "StateId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Form",
                newName: "FormStateId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ExpenseHistory",
                newName: "StateId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Expense",
                newName: "ExpenseStateId");

            migrationBuilder.AlterColumn<string>(
                name: "TrackingId",
                table: "Form",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "TrackingId",
                table: "Expense",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);

            migrationBuilder.CreateTable(
                name: "ExpenseState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormState",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormState", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ExpenseState",
                columns: new[] { "Id", "State" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.InsertData(
                table: "FormState",
                columns: new[] { "Id", "State" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Form_FormStateId",
                table: "Form",
                column: "FormStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ExpenseStateId",
                table: "Expense",
                column: "ExpenseStateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_ExpenseState_ExpenseStateId",
                table: "Expense",
                column: "ExpenseStateId",
                principalTable: "ExpenseState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Form_FormState_FormStateId",
                table: "Form",
                column: "FormStateId",
                principalTable: "FormState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
