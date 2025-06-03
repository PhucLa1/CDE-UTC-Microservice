using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class DefaultAccessStoragev2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FolderPermissions",
                newName: "TargetId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FilePermissions",
                newName: "TargetId");

            migrationBuilder.AddColumn<bool>(
                name: "IsGroup",
                table: "FolderPermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGroup",
                table: "FilePermissions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGroup",
                table: "FolderPermissions");

            migrationBuilder.DropColumn(
                name: "IsGroup",
                table: "FilePermissions");

            migrationBuilder.RenameColumn(
                name: "TargetId",
                table: "FolderPermissions",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "TargetId",
                table: "FilePermissions",
                newName: "UserId");
        }
    }
}
