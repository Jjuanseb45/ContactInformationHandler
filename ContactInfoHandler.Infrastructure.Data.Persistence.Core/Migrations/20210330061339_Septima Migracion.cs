using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class SeptimaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AreaOfWorkEntityAreaId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AreaOfWorkEntityAreaId",
                table: "Employees",
                column: "AreaOfWorkEntityAreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Areas_AreaOfWorkEntityAreaId",
                table: "Employees",
                column: "AreaOfWorkEntityAreaId",
                principalTable: "Areas",
                principalColumn: "AreaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Areas_AreaOfWorkEntityAreaId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AreaOfWorkEntityAreaId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AreaOfWorkEntityAreaId",
                table: "Employees");
        }
    }
}
