using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateV10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FolderHistorys_Name",
                table: "FolderHistorys");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FolderHistorys",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "FolderHistorys",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_FolderHistorys_Name",
                table: "FolderHistorys",
                column: "Name",
                unique: true);
        }
    }
}
