using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateV7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileHistories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileId = table.Column<int>(type: "int", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileVersion = table.Column<int>(type: "int", nullable: false),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    MimeType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created_by = table.Column<int>(type: "int", nullable: false),
                    updated_by = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileHistories", x => x.id);
                    table.ForeignKey(
                        name: "FK_FileHistories_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FolderTags_FolderId",
                table: "FolderTags",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_FileHistories_FileId",
                table: "FileHistories",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_FolderTags_Folders_FolderId",
                table: "FolderTags",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FolderTags_Folders_FolderId",
                table: "FolderTags");

            migrationBuilder.DropTable(
                name: "FileHistories");

            migrationBuilder.DropIndex(
                name: "IX_FolderTags_FolderId",
                table: "FolderTags");
        }
    }
}
