using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdProvider",
                table: "Employees",
                newName: "IdEmployee");

            migrationBuilder.RenameColumn(
                name: "IdProvider",
                table: "Customers",
                newName: "IdCustmer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdEmployee",
                table: "Employees",
                newName: "IdProvider");

            migrationBuilder.RenameColumn(
                name: "IdCustmer",
                table: "Customers",
                newName: "IdProvider");
        }
    }
}
