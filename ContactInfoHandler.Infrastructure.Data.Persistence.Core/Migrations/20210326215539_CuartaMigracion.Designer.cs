// <auto-generated />
using System;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ContactInfoHandler.Infrastructure.Data.Persistence.Core.Migrations
{
    [DbContext(typeof(ContextDb))]
    [Migration("20210326215539_CuartaMigracion")]
    partial class CuartaMigracion
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ContactInfoHandler.Dominio.Core.Areas.AreaOfWorkEntity", b =>
                {
                    b.Property<Guid>("AreaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AreaName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ResponsableEmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AreaId");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("ContactInfoHandler.Dominio.Core.Identifications.KindOfIdentificationEntity", b =>
                {
                    b.Property<Guid>("KindOfIdentificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IdentificationName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("KindOfIdentificationId");

                    b.ToTable("KindsOfId");
                });

            modelBuilder.Entity("ContactInfoHandler.Dominio.Core.Persons.Customers.CustomerEntity", b =>
                {
                    b.Property<Guid>("IdCustmer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FavoriteBrand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("IdNumber")
                        .HasColumnType("bigint");

                    b.Property<Guid>("KindOfIdentificationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("KindOfPerson")
                        .HasColumnType("int");

                    b.Property<string>("SecondLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("SignUpDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("IdCustmer");

                    b.HasIndex("KindOfIdentificationId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ContactInfoHandler.Dominio.Core.Persons.Employees.EmployeeEntity", b =>
                {
                    b.Property<Guid>("IdEmployee")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AreaId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("EmployeeCode")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("IdNumber")
                        .HasColumnType("bigint");

                    b.Property<Guid>("KindOfIdentificationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("KindOfPerson")
                        .HasColumnType("int");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<string>("SecondLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("SignUpDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("WorkPosition")
                        .HasColumnType("int");

                    b.HasKey("IdEmployee");

                    b.HasIndex("KindOfIdentificationId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ContactInfoHandler.Dominio.Core.Persons.Providers.ProviderEntity", b =>
                {
                    b.Property<Guid>("IdProvider")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ContactNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FirstLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("IdNumber")
                        .HasColumnType("bigint");

                    b.Property<Guid>("KindOfIdentificationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("KindOfPerson")
                        .HasColumnType("int");

                    b.Property<string>("SecondLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecondName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("SignUpDate")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("IdProvider");

                    b.HasIndex("KindOfIdentificationId");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("ContactInfoHandler.Dominio.Core.Persons.Customers.CustomerEntity", b =>
                {
                    b.HasOne("ContactInfoHandler.Dominio.Core.Identifications.KindOfIdentificationEntity", "KindOfIdentification")
                        .WithMany("CustomersList")
                        .HasForeignKey("KindOfIdentificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KindOfIdentification");
                });

            modelBuilder.Entity("ContactInfoHandler.Dominio.Core.Persons.Employees.EmployeeEntity", b =>
                {
                    b.HasOne("ContactInfoHandler.Dominio.Core.Identifications.KindOfIdentificationEntity", "KindOfIdentification")
                        .WithMany("EmployeeEntities")
                        .HasForeignKey("KindOfIdentificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KindOfIdentification");
                });

            modelBuilder.Entity("ContactInfoHandler.Dominio.Core.Persons.Providers.ProviderEntity", b =>
                {
                    b.HasOne("ContactInfoHandler.Dominio.Core.Identifications.KindOfIdentificationEntity", "KindOfIdentification")
                        .WithMany("ProvidersList")
                        .HasForeignKey("KindOfIdentificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KindOfIdentification");
                });

            modelBuilder.Entity("ContactInfoHandler.Dominio.Core.Identifications.KindOfIdentificationEntity", b =>
                {
                    b.Navigation("CustomersList");

                    b.Navigation("EmployeeEntities");

                    b.Navigation("ProvidersList");
                });
#pragma warning restore 612, 618
        }
    }
}
