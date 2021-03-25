using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Migrations
{
    public partial class FristMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    AreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.AreaId);
                });

            migrationBuilder.CreateTable(
                name: "KindsOfId",
                columns: table => new
                {
                    KindOfIdentificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdentificationName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KindsOfId", x => x.KindOfIdentificationId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    IdProvider = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FavoriteBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KindOfIdentificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNumber = table.Column<long>(type: "bigint", nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    KindOfPerson = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignUpDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.IdProvider);
                    table.ForeignKey(
                        name: "FK_Customers_KindsOfId_KindOfIdentificationId",
                        column: x => x.KindOfIdentificationId,
                        principalTable: "KindsOfId",
                        principalColumn: "KindOfIdentificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    IdProvider = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EmployeeCode = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KindOfPerson = table.Column<int>(type: "int", nullable: false),
                    WorkPosition = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    AreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AreaOfWorkAreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    KindOfIdentificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNumber = table.Column<long>(type: "bigint", nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignUpDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.IdProvider);
                    table.ForeignKey(
                        name: "FK_Employees_Areas_AreaOfWorkAreaId",
                        column: x => x.AreaOfWorkAreaId,
                        principalTable: "Areas",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_KindsOfId_KindOfIdentificationId",
                        column: x => x.KindOfIdentificationId,
                        principalTable: "KindsOfId",
                        principalColumn: "KindOfIdentificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    IdProvider = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<long>(type: "bigint", nullable: false),
                    KindOfIdentificationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdNumber = table.Column<long>(type: "bigint", nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    KindOfPerson = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondLastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignUpDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.IdProvider);
                    table.ForeignKey(
                        name: "FK_Providers_KindsOfId_KindOfIdentificationId",
                        column: x => x.KindOfIdentificationId,
                        principalTable: "KindsOfId",
                        principalColumn: "KindOfIdentificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_KindOfIdentificationId",
                table: "Customers",
                column: "KindOfIdentificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AreaOfWorkAreaId",
                table: "Employees",
                column: "AreaOfWorkAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_KindOfIdentificationId",
                table: "Employees",
                column: "KindOfIdentificationId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_KindOfIdentificationId",
                table: "Providers",
                column: "KindOfIdentificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "KindsOfId");
        }
    }
}
