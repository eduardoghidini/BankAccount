using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAccount.Warren.Database.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_name = table.Column<string>(maxLength: 20, nullable: true),
                    password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    owner_name = table.Column<string>(maxLength: 500, nullable: false),
                    account_number = table.Column<string>(maxLength: 20, nullable: false),
                    current_balance = table.Column<decimal>(nullable: false),
                    applied_balance = table.Column<decimal>(nullable: false),
                    user_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.id);
                    table.ForeignKey(
                        name: "FK_account_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "account_operation",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    operation_type = table.Column<int>(nullable: false),
                    note = table.Column<string>(maxLength: 130, nullable: true),
                    account_id = table.Column<int>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    operation_date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_operation", x => x.id);
                    table.ForeignKey(
                        name: "FK_account_operation_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "account_operation_request",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    operation_type = table.Column<int>(nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    creation_date = table.Column<DateTime>(nullable: false),
                    operation_date = table.Column<DateTime>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    note = table.Column<string>(maxLength: 130, nullable: true),
                    account_id = table.Column<int>(nullable: false),
                    operation_response_message = table.Column<string>(maxLength: 200, nullable: true),
                    job_reference_id = table.Column<string>(maxLength: 15, nullable: true),
                    processed_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_operation_request", x => x.id);
                    table.ForeignKey(
                        name: "FK_account_operation_request_account_account_id",
                        column: x => x.account_id,
                        principalTable: "account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_user_id",
                table: "account",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_account_operation_account_id",
                table: "account_operation",
                column: "account_id");

            migrationBuilder.CreateIndex(
                name: "IX_account_operation_request_account_id",
                table: "account_operation_request",
                column: "account_id");

            migrationBuilder.InsertData("user", columns: new[] { "id", "user_name", "password" }, new object[] { 1, "my_user", "abc@123" });

            migrationBuilder.InsertData("account", columns: new[] { "id", "owner_name", "current_balance", "applied_balance", "account_number","user_id" }, 
                new object[] { 1, "Default Account", 100, 0, "921855", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account_operation");

            migrationBuilder.DropTable(
                name: "account_operation_request");

            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
