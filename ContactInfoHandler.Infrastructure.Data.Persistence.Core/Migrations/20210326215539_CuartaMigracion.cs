using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class CuartaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Areas_AreaOfWorkAreaId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AreaOfWorkAreaId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AreaOfWorkAreaId",
                table: "Employees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AreaOfWorkAreaId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AreaOfWorkAreaId",
                table: "Employees",
                column: "AreaOfWorkAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Areas_AreaOfWorkAreaId",
                table: "Employees",
                column: "AreaOfWorkAreaId",
                principalTable: "Areas",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
