using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalHelpSystem.Data.Migrations
{
    public partial class AddReviewEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "LegalAdvisors");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TextReview = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Stars = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UploaderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DocumentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LegalAdvisorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LegalAdviseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Documents_DocumentId",
                        column: x => x.DocumentId,
                        principalTable: "Documents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_LegalAdvises_LegalAdviseId",
                        column: x => x.LegalAdviseId,
                        principalTable: "LegalAdvises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_LegalAdvisors_LegalAdvisorId",
                        column: x => x.LegalAdvisorId,
                        principalTable: "LegalAdvisors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Uploaders_UploaderId",
                        column: x => x.UploaderId,
                        principalTable: "Uploaders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_DocumentId",
                table: "Reviews",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_LegalAdviseId",
                table: "Reviews",
                column: "LegalAdviseId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_LegalAdvisorId",
                table: "Reviews",
                column: "LegalAdvisorId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UploaderId",
                table: "Reviews",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "LegalAdvisors",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);
        }
    }
}
