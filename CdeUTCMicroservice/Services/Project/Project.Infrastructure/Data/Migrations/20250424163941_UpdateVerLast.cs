using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVerLast : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Priorities_Characteristic_PriorityId",
                table: "Todos");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Statuses_Characteristic_StatusId",
                table: "Todos");

            migrationBuilder.DropForeignKey(
                name: "FK_Todos_Types_Characteristic_TypeId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_Characteristic_PriorityId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_Characteristic_StatusId",
                table: "Todos");

            migrationBuilder.DropIndex(
                name: "IX_Todos_Characteristic_TypeId",
                table: "Todos");

            migrationBuilder.RenameColumn(
                name: "Characteristic_TypeId",
                table: "Todos",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Characteristic_StatusId",
                table: "Todos",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "Characteristic_PriorityId",
                table: "Todos",
                newName: "PriorityId");

            migrationBuilder.AddColumn<int>(
                name: "IsAssignToGroup",
                table: "Todos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Todos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAssignToGroup",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Todos");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Todos",
                newName: "Characteristic_TypeId");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Todos",
                newName: "Characteristic_StatusId");

            migrationBuilder.RenameColumn(
                name: "PriorityId",
                table: "Todos",
                newName: "Characteristic_PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Characteristic_PriorityId",
                table: "Todos",
                column: "Characteristic_PriorityId");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Characteristic_StatusId",
                table: "Todos",
                column: "Characteristic_StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Todos_Characteristic_TypeId",
                table: "Todos",
                column: "Characteristic_TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Priorities_Characteristic_PriorityId",
                table: "Todos",
                column: "Characteristic_PriorityId",
                principalTable: "Priorities",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Statuses_Characteristic_StatusId",
                table: "Todos",
                column: "Characteristic_StatusId",
                principalTable: "Statuses",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Todos_Types_Characteristic_TypeId",
                table: "Todos",
                column: "Characteristic_TypeId",
                principalTable: "Types",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
