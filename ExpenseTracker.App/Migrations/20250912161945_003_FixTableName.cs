using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.App.Migrations
{
    /// <inheritdoc />
    public partial class _003_FixTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Principals_PrincipalId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseHistories_Employees_ActorId",
                table: "ExpenseHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseHistories_Expenses_ExpenseId",
                table: "ExpenseHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_ExpenseStates_ExpenseStateId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Forms_FormId",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK_FormHistories_Employees_ActorId",
                table: "FormHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_FormHistories_Forms_FormId",
                table: "FormHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Currencies_CurrencyId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_FormStates_FormStateId",
                table: "Forms");

            migrationBuilder.DropForeignKey(
                name: "FK_LoginHistories_UserRoles_UserRoleId",
                table: "LoginHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Principals_PrincipalId",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Principals",
                table: "Principals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginHistories",
                table: "LoginHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormStates",
                table: "FormStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forms",
                table: "Forms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormHistories",
                table: "FormHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseStates",
                table: "ExpenseStates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseHistories",
                table: "ExpenseHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRole");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Role");

            migrationBuilder.RenameTable(
                name: "Principals",
                newName: "Principal");

            migrationBuilder.RenameTable(
                name: "LoginHistories",
                newName: "LoginHistory");

            migrationBuilder.RenameTable(
                name: "FormStates",
                newName: "FormState");

            migrationBuilder.RenameTable(
                name: "Forms",
                newName: "Form");

            migrationBuilder.RenameTable(
                name: "FormHistories",
                newName: "FormHistory");

            migrationBuilder.RenameTable(
                name: "ExpenseStates",
                newName: "ExpenseState");

            migrationBuilder.RenameTable(
                name: "Expenses",
                newName: "Expense");

            migrationBuilder.RenameTable(
                name: "ExpenseHistories",
                newName: "ExpenseHistory");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "Currencies",
                newName: "Currency");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRole",
                newName: "IX_UserRole_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRoles_PrincipalId_RoleId",
                table: "UserRole",
                newName: "IX_UserRole_PrincipalId_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_Name",
                table: "Role",
                newName: "IX_Role_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Principals_Username",
                table: "Principal",
                newName: "IX_Principal_Username");

            migrationBuilder.RenameIndex(
                name: "IX_LoginHistories_UserRoleId",
                table: "LoginHistory",
                newName: "IX_LoginHistory_UserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_TrackingId",
                table: "Form",
                newName: "IX_Form_TrackingId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_FormStateId",
                table: "Form",
                newName: "IX_Form_FormStateId");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_CurrencyId",
                table: "Form",
                newName: "IX_Form_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_FormHistories_FormId",
                table: "FormHistory",
                newName: "IX_FormHistory_FormId");

            migrationBuilder.RenameIndex(
                name: "IX_FormHistories_ActorId",
                table: "FormHistory",
                newName: "IX_FormHistory_ActorId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_TrackingId",
                table: "Expense",
                newName: "IX_Expense_TrackingId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_FormId",
                table: "Expense",
                newName: "IX_Expense_FormId");

            migrationBuilder.RenameIndex(
                name: "IX_Expenses_ExpenseStateId",
                table: "Expense",
                newName: "IX_Expense_ExpenseStateId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseHistories_ExpenseId",
                table: "ExpenseHistory",
                newName: "IX_ExpenseHistory_ExpenseId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseHistories_ActorId",
                table: "ExpenseHistory",
                newName: "IX_ExpenseHistory_ActorId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_PrincipalId",
                table: "Employee",
                newName: "IX_Employee_PrincipalId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_ManagerId",
                table: "Employee",
                newName: "IX_Employee_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_Code",
                table: "Employee",
                newName: "IX_Employee_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Role",
                table: "Role",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Principal",
                table: "Principal",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginHistory",
                table: "LoginHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormState",
                table: "FormState",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Form",
                table: "Form",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormHistory",
                table: "FormHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseState",
                table: "ExpenseState",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expense",
                table: "Expense",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseHistory",
                table: "ExpenseHistory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currency",
                table: "Currency",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Employee_ManagerId",
                table: "Employee",
                column: "ManagerId",
                principalTable: "Employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Principal_PrincipalId",
                table: "Employee",
                column: "PrincipalId",
                principalTable: "Principal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_ExpenseState_ExpenseStateId",
                table: "Expense",
                column: "ExpenseStateId",
                principalTable: "ExpenseState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Form_FormId",
                table: "Expense",
                column: "FormId",
                principalTable: "Form",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseHistory_Employee_ActorId",
                table: "ExpenseHistory",
                column: "ActorId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseHistory_Expense_ExpenseId",
                table: "ExpenseHistory",
                column: "ExpenseId",
                principalTable: "Expense",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Form_Currency_CurrencyId",
                table: "Form",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Form_FormState_FormStateId",
                table: "Form",
                column: "FormStateId",
                principalTable: "FormState",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormHistory_Employee_ActorId",
                table: "FormHistory",
                column: "ActorId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormHistory_Form_FormId",
                table: "FormHistory",
                column: "FormId",
                principalTable: "Form",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginHistory_UserRole_UserRoleId",
                table: "LoginHistory",
                column: "UserRoleId",
                principalTable: "UserRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Principal_PrincipalId",
                table: "UserRole",
                column: "PrincipalId",
                principalTable: "Principal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Employee_ManagerId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Principal_PrincipalId",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_ExpenseState_ExpenseStateId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Form_FormId",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseHistory_Employee_ActorId",
                table: "ExpenseHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_ExpenseHistory_Expense_ExpenseId",
                table: "ExpenseHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Form_Currency_CurrencyId",
                table: "Form");

            migrationBuilder.DropForeignKey(
                name: "FK_Form_FormState_FormStateId",
                table: "Form");

            migrationBuilder.DropForeignKey(
                name: "FK_FormHistory_Employee_ActorId",
                table: "FormHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_FormHistory_Form_FormId",
                table: "FormHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_LoginHistory_UserRole_UserRoleId",
                table: "LoginHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Principal_PrincipalId",
                table: "UserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Role",
                table: "Role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Principal",
                table: "Principal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginHistory",
                table: "LoginHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormState",
                table: "FormState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FormHistory",
                table: "FormHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Form",
                table: "Form");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseState",
                table: "ExpenseState");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExpenseHistory",
                table: "ExpenseHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expense",
                table: "Expense");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Currency",
                table: "Currency");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "UserRoles");

            migrationBuilder.RenameTable(
                name: "Role",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Principal",
                newName: "Principals");

            migrationBuilder.RenameTable(
                name: "LoginHistory",
                newName: "LoginHistories");

            migrationBuilder.RenameTable(
                name: "FormState",
                newName: "FormStates");

            migrationBuilder.RenameTable(
                name: "FormHistory",
                newName: "FormHistories");

            migrationBuilder.RenameTable(
                name: "Form",
                newName: "Forms");

            migrationBuilder.RenameTable(
                name: "ExpenseState",
                newName: "ExpenseStates");

            migrationBuilder.RenameTable(
                name: "ExpenseHistory",
                newName: "ExpenseHistories");

            migrationBuilder.RenameTable(
                name: "Expense",
                newName: "Expenses");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "Currency",
                newName: "Currencies");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_PrincipalId_RoleId",
                table: "UserRoles",
                newName: "IX_UserRoles_PrincipalId_RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Role_Name",
                table: "Roles",
                newName: "IX_Roles_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Principal_Username",
                table: "Principals",
                newName: "IX_Principals_Username");

            migrationBuilder.RenameIndex(
                name: "IX_LoginHistory_UserRoleId",
                table: "LoginHistories",
                newName: "IX_LoginHistories_UserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_FormHistory_FormId",
                table: "FormHistories",
                newName: "IX_FormHistories_FormId");

            migrationBuilder.RenameIndex(
                name: "IX_FormHistory_ActorId",
                table: "FormHistories",
                newName: "IX_FormHistories_ActorId");

            migrationBuilder.RenameIndex(
                name: "IX_Form_TrackingId",
                table: "Forms",
                newName: "IX_Forms_TrackingId");

            migrationBuilder.RenameIndex(
                name: "IX_Form_FormStateId",
                table: "Forms",
                newName: "IX_Forms_FormStateId");

            migrationBuilder.RenameIndex(
                name: "IX_Form_CurrencyId",
                table: "Forms",
                newName: "IX_Forms_CurrencyId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseHistory_ExpenseId",
                table: "ExpenseHistories",
                newName: "IX_ExpenseHistories_ExpenseId");

            migrationBuilder.RenameIndex(
                name: "IX_ExpenseHistory_ActorId",
                table: "ExpenseHistories",
                newName: "IX_ExpenseHistories_ActorId");

            migrationBuilder.RenameIndex(
                name: "IX_Expense_TrackingId",
                table: "Expenses",
                newName: "IX_Expenses_TrackingId");

            migrationBuilder.RenameIndex(
                name: "IX_Expense_FormId",
                table: "Expenses",
                newName: "IX_Expenses_FormId");

            migrationBuilder.RenameIndex(
                name: "IX_Expense_ExpenseStateId",
                table: "Expenses",
                newName: "IX_Expenses_ExpenseStateId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_PrincipalId",
                table: "Employees",
                newName: "IX_Employees_PrincipalId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_ManagerId",
                table: "Employees",
                newName: "IX_Employees_ManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_Employee_Code",
                table: "Employees",
                newName: "IX_Employees_Code");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Principals",
                table: "Principals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginHistories",
                table: "LoginHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormStates",
                table: "FormStates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FormHistories",
                table: "FormHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forms",
                table: "Forms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseStates",
                table: "ExpenseStates",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExpenseHistories",
                table: "ExpenseHistories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expenses",
                table: "Expenses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Currencies",
                table: "Currencies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Principals_PrincipalId",
                table: "Employees",
                column: "PrincipalId",
                principalTable: "Principals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseHistories_Employees_ActorId",
                table: "ExpenseHistories",
                column: "ActorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExpenseHistories_Expenses_ExpenseId",
                table: "ExpenseHistories",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_ExpenseStates_ExpenseStateId",
                table: "Expenses",
                column: "ExpenseStateId",
                principalTable: "ExpenseStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Forms_FormId",
                table: "Expenses",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormHistories_Employees_ActorId",
                table: "FormHistories",
                column: "ActorId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FormHistories_Forms_FormId",
                table: "FormHistories",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Currencies_CurrencyId",
                table: "Forms",
                column: "CurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_FormStates_FormStateId",
                table: "Forms",
                column: "FormStateId",
                principalTable: "FormStates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoginHistories_UserRoles_UserRoleId",
                table: "LoginHistories",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Principals_PrincipalId",
                table: "UserRoles",
                column: "PrincipalId",
                principalTable: "Principals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoles_Roles_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
