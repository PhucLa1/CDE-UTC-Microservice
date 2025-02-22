using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateV8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullPath",
                table: "Folders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FullPath",
                table: "Files",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_FullPath",
                table: "Folders",
                column: "FullPath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_FullPath",
                table: "Files",
                column: "FullPath",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Folders_FullPath",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Files_FullPath",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FullPath",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "FullPath",
                table: "Files");
        }
    }
}
