using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Event.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Activities",
                newName: "ResourceId");

            migrationBuilder.AddColumn<int>(
                name: "ActivityTypeCategory",
                table: "ActivityTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActivityTypeMode",
                table: "ActivityTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ActivityTypeParentCategory",
                table: "ActivityTypeParents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "ActivityTypeParents",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityTypeCategory",
                table: "ActivityTypes");

            migrationBuilder.DropColumn(
                name: "ActivityTypeMode",
                table: "ActivityTypes");

            migrationBuilder.DropColumn(
                name: "ActivityTypeParentCategory",
                table: "ActivityTypeParents");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ActivityTypeParents");

            migrationBuilder.RenameColumn(
                name: "ResourceId",
                table: "Activities",
                newName: "ProjectId");
        }
    }
}
