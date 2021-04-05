using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class Sextamigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_KindsOfId_KindOfIdentificationId",
                table: "Providers");

            migrationBuilder.DropIndex(
                name: "IX_Providers_KindOfIdentificationId",
                table: "Providers");

            migrationBuilder.AddColumn<Guid>(
                name: "KindOfIdentificationEntityKindOfIdentificationId",
                table: "Providers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Providers_KindOfIdentificationEntityKindOfIdentificationId",
                table: "Providers",
                column: "KindOfIdentificationEntityKindOfIdentificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_KindsOfId_KindOfIdentificationEntityKindOfIdentificationId",
                table: "Providers",
                column: "KindOfIdentificationEntityKindOfIdentificationId",
                principalTable: "KindsOfId",
                principalColumn: "KindOfIdentificationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Providers_KindsOfId_KindOfIdentificationEntityKindOfIdentificationId",
                table: "Providers");

            migrationBuilder.DropIndex(
                name: "IX_Providers_KindOfIdentificationEntityKindOfIdentificationId",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "KindOfIdentificationEntityKindOfIdentificationId",
                table: "Providers");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_KindOfIdentificationId",
                table: "Providers",
                column: "KindOfIdentificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Providers_KindsOfId_KindOfIdentificationId",
                table: "Providers",
                column: "KindOfIdentificationId",
                principalTable: "KindsOfId",
                principalColumn: "KindOfIdentificationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
