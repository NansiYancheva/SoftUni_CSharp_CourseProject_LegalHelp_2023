using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalHelpSystem.Data.Migrations
{
    public partial class AddingDocumentReferenceToTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Tickets",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TicketId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_DocumentId",
                table: "Tickets",
                column: "DocumentId",
                unique: true,
                filter: "[DocumentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Documents_DocumentId",
                table: "Tickets",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Documents_DocumentId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_DocumentId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "Documents");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "Tickets",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
