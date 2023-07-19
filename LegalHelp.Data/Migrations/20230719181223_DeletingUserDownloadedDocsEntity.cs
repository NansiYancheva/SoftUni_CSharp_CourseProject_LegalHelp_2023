using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalHelpSystem.Data.Migrations
{
    public partial class DeletingUserDownloadedDocsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDocumentDownloads");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserDocumentDownloads",
                columns: table => new
                {
                    DownloadedDocsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DownloadersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDocumentDownloads", x => new { x.DownloadedDocsId, x.DownloadersId });
                    table.ForeignKey(
                        name: "FK_UserDocumentDownloads_AspNetUsers_DownloadersId",
                        column: x => x.DownloadersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDocumentDownloads_Documents_DownloadedDocsId",
                        column: x => x.DownloadedDocsId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDocumentDownloads_DownloadersId",
                table: "UserDocumentDownloads",
                column: "DownloadersId");
        }
    }
}
