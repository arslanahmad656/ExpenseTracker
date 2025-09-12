using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ExpenseTracker.App.Migrations
{
    /// <inheritdoc />
    public partial class _002_InitialSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Principals",
                type: "nvarchar(127)",
                maxLength: 127,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(31)",
                oldMaxLength: 31);

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "Id", "Code", "FullName", "Symbol" },
                values: new object[,]
                {
                    { 1, "USD", "US Dollar", "$" },
                    { 2, "EUR", "Euro", "€" },
                    { 3, "GBP", "British Pound", "£" },
                    { 4, "INR", "Indian Rupee", "₹" },
                    { 5, "PKR", "Pakistani Rupee", "₨" }
                });

            migrationBuilder.InsertData(
                table: "ExpenseStates",
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
                table: "FormStates",
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
                table: "Principals",
                columns: new[] { "Id", "IsActive", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, true, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "emp1" },
                    { 2, false, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "emp2" },
                    { 3, true, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "emp3" },
                    { 4, false, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "emp4" },
                    { 5, true, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "emp5" },
                    { 6, false, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "emp6" },
                    { 7, true, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "emp7" },
                    { 8, false, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "emp8" },
                    { 9, true, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "emp9" },
                    { 10, false, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "acc1" },
                    { 11, true, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "acc2" },
                    { 12, false, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "acc3" },
                    { 13, true, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "acc4" },
                    { 14, false, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "acc5" },
                    { 15, true, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "acc6" },
                    { 16, false, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "acc7" },
                    { 17, true, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "acc8" },
                    { 18, false, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "acc9" },
                    { 19, true, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "adm1" },
                    { 20, false, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "adm2" },
                    { 21, true, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "adm3" },
                    { 22, false, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "adm4" },
                    { 23, true, "$2a$11$BcmWuSKqCzwxqaID7uyke.zEsmY5EPaQbXRAEaIzbhUG6nkIDbt4e", "adm5" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Employee" },
                    { 2, "Manager" },
                    { 3, "Accountant" },
                    { 4, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Code", "HireDate", "ManagerId", "Name", "PrincipalId" },
                values: new object[,]
                {
                    { 1, "EMP1", new DateTimeOffset(new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "emp1", 1 },
                    { 2, "EMP2", new DateTimeOffset(new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "emp2", 2 },
                    { 3, "EMP3", new DateTimeOffset(new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "emp3", 3 },
                    { 10, "ACC1", new DateTimeOffset(new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "acc1", 10 },
                    { 11, "ACC2", new DateTimeOffset(new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "acc2", 11 },
                    { 12, "ACC3", new DateTimeOffset(new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "acc3", 12 },
                    { 13, "ACC4", new DateTimeOffset(new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "acc4", 13 },
                    { 14, "ACC5", new DateTimeOffset(new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "acc5", 14 },
                    { 15, "ACC6", new DateTimeOffset(new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "acc6", 15 },
                    { 16, "ACC7", new DateTimeOffset(new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "acc7", 16 },
                    { 17, "ACC8", new DateTimeOffset(new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "acc8", 17 },
                    { 18, "ACC9", new DateTimeOffset(new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "acc9", 18 },
                    { 19, "ADM1", new DateTimeOffset(new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "adm1", 19 },
                    { 20, "ADM2", new DateTimeOffset(new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "adm2", 20 },
                    { 21, "ADM3", new DateTimeOffset(new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "adm3", 21 },
                    { 22, "ADM4", new DateTimeOffset(new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "adm4", 22 },
                    { 23, "ADM5", new DateTimeOffset(new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), null, "adm5", 23 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "PrincipalId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 4, 1 },
                    { 5, 5, 1 },
                    { 6, 6, 1 },
                    { 7, 7, 1 },
                    { 8, 8, 1 },
                    { 9, 9, 1 },
                    { 10, 10, 3 },
                    { 11, 11, 3 },
                    { 12, 12, 3 },
                    { 13, 13, 3 },
                    { 14, 14, 3 },
                    { 15, 15, 3 },
                    { 16, 16, 3 },
                    { 17, 17, 3 },
                    { 18, 18, 3 },
                    { 19, 19, 4 },
                    { 20, 20, 4 },
                    { 21, 21, 4 },
                    { 22, 22, 4 },
                    { 23, 23, 4 },
                    { 24, 1, 2 },
                    { 25, 2, 2 },
                    { 26, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Code", "HireDate", "ManagerId", "Name", "PrincipalId" },
                values: new object[,]
                {
                    { 4, "EMP4", new DateTimeOffset(new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), 1, "emp4", 4 },
                    { 5, "EMP5", new DateTimeOffset(new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), 1, "emp5", 5 },
                    { 6, "EMP6", new DateTimeOffset(new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), 2, "emp6", 6 },
                    { 7, "EMP7", new DateTimeOffset(new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), 2, "emp7", 7 },
                    { 8, "EMP8", new DateTimeOffset(new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), 3, "emp8", 8 },
                    { 9, "EMP9", new DateTimeOffset(new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 5, 0, 0, 0)), 3, "emp9", 9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees");

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ExpenseStates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ExpenseStates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ExpenseStates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ExpenseStates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ExpenseStates",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "FormStates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "FormStates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "FormStates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "FormStates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "FormStates",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Principals",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Principals",
                type: "nvarchar(31)",
                maxLength: 31,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(127)",
                oldMaxLength: 127);
        }
    }
}
