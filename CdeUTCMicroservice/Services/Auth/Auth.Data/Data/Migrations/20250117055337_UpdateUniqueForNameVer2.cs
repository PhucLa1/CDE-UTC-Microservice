using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Data.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUniqueForNameVer2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UUID",
                table: "JobTitles",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Name",
                table: "Languages",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobTitles_Name",
                table: "JobTitles",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Languages_Name",
                table: "Languages");

            migrationBuilder.DropIndex(
                name: "IX_JobTitles_Name",
                table: "JobTitles");

            migrationBuilder.AlterColumn<string>(
                name: "UUID",
                table: "JobTitles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);
        }
    }
}
