using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalHelpSystem.Data.Migrations
{
    public partial class RemoveApplicationUserDocumentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserDocument");

            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DocumentId",
                table: "AspNetUsers",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Documents_DocumentId",
                table: "AspNetUsers",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Documents_DocumentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DocumentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ApplicationUserDocument",
                columns: table => new
                {
                    DownloadedDocumentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DownloadersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserDocument", x => new { x.DownloadedDocumentsId, x.DownloadersId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserDocument_AspNetUsers_DownloadersId",
                        column: x => x.DownloadersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserDocument_Documents_DownloadedDocumentsId",
                        column: x => x.DownloadedDocumentsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserDocument_DownloadersId",
                table: "ApplicationUserDocument",
                column: "DownloadersId");
        }
    }
}
