using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class OMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_KindsOfId_KindOfIdentificationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_KindOfIdentificationId",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "KindOfIdentificationEntityKindOfIdentificationId",
                table: "Employees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_KindOfIdentificationEntityKindOfIdentificationId",
                table: "Employees",
                column: "KindOfIdentificationEntityKindOfIdentificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_KindsOfId_KindOfIdentificationEntityKindOfIdentificationId",
                table: "Employees",
                column: "KindOfIdentificationEntityKindOfIdentificationId",
                principalTable: "KindsOfId",
                principalColumn: "KindOfIdentificationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_KindsOfId_KindOfIdentificationEntityKindOfIdentificationId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_KindOfIdentificationEntityKindOfIdentificationId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "KindOfIdentificationEntityKindOfIdentificationId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_KindOfIdentificationId",
                table: "Employees",
                column: "KindOfIdentificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_KindsOfId_KindOfIdentificationId",
                table: "Employees",
                column: "KindOfIdentificationId",
                principalTable: "KindsOfId",
                principalColumn: "KindOfIdentificationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
