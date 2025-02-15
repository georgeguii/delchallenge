using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankAccounts.Infra.Migrations
{
    /// <inheritdoc />
    public partial class BankAccountsAndBalanceEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "bankaccounts");

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                schema: "bankaccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Branch = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HolderName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HolderEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HolderDocument = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    HolderType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Balances",
                schema: "bankaccounts",
                columns: table => new
                {
                    BankAccountId = table.Column<int>(type: "int", nullable: false),
                    AvailableAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    BlockedAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Balances", x => x.BankAccountId);
                    table.ForeignKey(
                        name: "FK_Balances_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalSchema: "bankaccounts",
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_Number",
                schema: "bankaccounts",
                table: "BankAccounts",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balances",
                schema: "bankaccounts");

            migrationBuilder.DropTable(
                name: "BankAccounts",
                schema: "bankaccounts");
        }
    }
}
