using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.App.Migrations
{
    /// <inheritdoc />
    public partial class _001_Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Principals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Principals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrackingId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    FormStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forms_Currencies_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currencies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Forms_FormStates_FormStateId",
                        column: x => x.FormStateId,
                        principalTable: "FormStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    HireDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Principals_PrincipalId",
                        column: x => x.PrincipalId,
                        principalTable: "Principals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrincipalId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Principals_PrincipalId",
                        column: x => x.PrincipalId,
                        principalTable: "Principals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    TrackingId = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ExpenseStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_ExpenseStates_ExpenseStateId",
                        column: x => x.ExpenseStateId,
                        principalTable: "ExpenseStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Expenses_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    RecordedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormHistories_Employees_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormHistories_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LoginHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRoleId = table.Column<int>(type: "int", nullable: false),
                    LoginTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LogoutTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginHistories_UserRoles_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseHistories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpenseId = table.Column<int>(type: "int", nullable: false),
                    RecordedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExpenseHistories_Employees_ActorId",
                        column: x => x.ActorId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExpenseHistories_Expenses_ExpenseId",
                        column: x => x.ExpenseId,
                        principalTable: "Expenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Code",
                table: "Employees",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PrincipalId",
                table: "Employees",
                column: "PrincipalId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseHistories_ActorId",
                table: "ExpenseHistories",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExpenseHistories_ExpenseId",
                table: "ExpenseHistories",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_ExpenseStateId",
                table: "Expenses",
                column: "ExpenseStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_FormId",
                table: "Expenses",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_TrackingId",
                table: "Expenses",
                column: "TrackingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FormHistories_ActorId",
                table: "FormHistories",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_FormHistories_FormId",
                table: "FormHistories",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_CurrencyId",
                table: "Forms",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_FormStateId",
                table: "Forms",
                column: "FormStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_TrackingId",
                table: "Forms",
                column: "TrackingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LoginHistories_UserRoleId",
                table: "LoginHistories",
                column: "UserRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Principals_Username",
                table: "Principals",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_PrincipalId_RoleId",
                table: "UserRoles",
                columns: new[] { "PrincipalId", "RoleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExpenseHistories");

            migrationBuilder.DropTable(
                name: "FormHistories");

            migrationBuilder.DropTable(
                name: "LoginHistories");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "ExpenseStates");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "Principals");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "FormStates");
        }
    }
}
