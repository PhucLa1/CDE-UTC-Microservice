using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateV9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Folders_Name",
                table: "Folders");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Folders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "FullPathName",
                table: "Folders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_FullPathName",
                table: "Folders",
                column: "FullPathName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Folders_FullPathName",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "FullPathName",
                table: "Folders");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Folders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_Name",
                table: "Folders",
                column: "Name",
                unique: true);
        }
    }
}
