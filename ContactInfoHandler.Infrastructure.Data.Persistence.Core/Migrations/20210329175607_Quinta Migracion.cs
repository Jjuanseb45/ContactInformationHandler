using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class QuintaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_KindsOfId_KindOfIdentificationId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_KindOfIdentificationId",
                table: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "KindOfIdentificationEntityKindOfIdentificationId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_KindOfIdentificationEntityKindOfIdentificationId",
                table: "Customers",
                column: "KindOfIdentificationEntityKindOfIdentificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_KindsOfId_KindOfIdentificationEntityKindOfIdentificationId",
                table: "Customers",
                column: "KindOfIdentificationEntityKindOfIdentificationId",
                principalTable: "KindsOfId",
                principalColumn: "KindOfIdentificationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_KindsOfId_KindOfIdentificationEntityKindOfIdentificationId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_KindOfIdentificationEntityKindOfIdentificationId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "KindOfIdentificationEntityKindOfIdentificationId",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_KindOfIdentificationId",
                table: "Customers",
                column: "KindOfIdentificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_KindsOfId_KindOfIdentificationId",
                table: "Customers",
                column: "KindOfIdentificationId",
                principalTable: "KindsOfId",
                principalColumn: "KindOfIdentificationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
