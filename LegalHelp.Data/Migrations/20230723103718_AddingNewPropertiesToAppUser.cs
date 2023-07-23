using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalHelpSystem.Data.Migrations
{
    public partial class AddingNewPropertiesToAppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "Test");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "Testov");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ApplicationUserId",
                table: "Documents",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AspNetUsers_ApplicationUserId",
                table: "Documents",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AspNetUsers_ApplicationUserId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ApplicationUserId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
