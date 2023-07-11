using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalHelpSystem.Data.Migrations
{
    public partial class ChangingDocumentDownloadersTrack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_AspNetUsers_DownloaderId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_DownloaderId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "DownloaderId",
                table: "Documents");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserDocument");

            migrationBuilder.AddColumn<Guid>(
                name: "DownloaderId",
                table: "Documents",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DownloaderId",
                table: "Documents",
                column: "DownloaderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_AspNetUsers_DownloaderId",
                table: "Documents",
                column: "DownloaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
