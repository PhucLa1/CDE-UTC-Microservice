using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateV11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Folders_FullPath",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_FullPathName",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Files_FullPath",
                table: "Files");

            migrationBuilder.AlterColumn<string>(
                name: "FullPathName",
                table: "Folders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FullPath",
                table: "Folders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FullPath",
                table: "Files",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FullPathName",
                table: "Folders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FullPath",
                table: "Folders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FullPath",
                table: "Files",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_FullPath",
                table: "Folders",
                column: "FullPath",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Folders_FullPathName",
                table: "Folders",
                column: "FullPathName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_FullPath",
                table: "Files",
                column: "FullPath",
                unique: true);
        }
    }
}
