﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankAccount.Warren.Database.Migrations
{
    public partial class AddOperationDateColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "operation_date",
                table: "account_operation",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "operation_date",
                table: "account_operation");
        }
    }
}
