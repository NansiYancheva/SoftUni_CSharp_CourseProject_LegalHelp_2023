using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalHelpSystem.Data.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserDocument_AspNetUsers_DownloadersId",
                table: "ApplicationUserDocument");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserDocument_Documents_DownloadedDocsId",
                table: "ApplicationUserDocument");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserDocument",
                table: "ApplicationUserDocument");

            migrationBuilder.RenameTable(
                name: "ApplicationUserDocument",
                newName: "UserDocumentDownloads");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserDocument_DownloadersId",
                table: "UserDocumentDownloads",
                newName: "IX_UserDocumentDownloads_DownloadersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserDocumentDownloads",
                table: "UserDocumentDownloads",
                columns: new[] { "DownloadedDocsId", "DownloadersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserDocumentDownloads_AspNetUsers_DownloadersId",
                table: "UserDocumentDownloads",
                column: "DownloadersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserDocumentDownloads_Documents_DownloadedDocsId",
                table: "UserDocumentDownloads",
                column: "DownloadedDocsId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDocumentDownloads_AspNetUsers_DownloadersId",
                table: "UserDocumentDownloads");

            migrationBuilder.DropForeignKey(
                name: "FK_UserDocumentDownloads_Documents_DownloadedDocsId",
                table: "UserDocumentDownloads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserDocumentDownloads",
                table: "UserDocumentDownloads");

            migrationBuilder.RenameTable(
                name: "UserDocumentDownloads",
                newName: "ApplicationUserDocument");

            migrationBuilder.RenameIndex(
                name: "IX_UserDocumentDownloads_DownloadersId",
                table: "ApplicationUserDocument",
                newName: "IX_ApplicationUserDocument_DownloadersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserDocument",
                table: "ApplicationUserDocument",
                columns: new[] { "DownloadedDocsId", "DownloadersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserDocument_AspNetUsers_DownloadersId",
                table: "ApplicationUserDocument",
                column: "DownloadersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserDocument_Documents_DownloadedDocsId",
                table: "ApplicationUserDocument",
                column: "DownloadedDocsId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
