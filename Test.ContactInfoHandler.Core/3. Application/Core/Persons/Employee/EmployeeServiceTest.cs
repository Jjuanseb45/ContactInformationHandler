using ContactInfoHandler.Application.Core.Persons.Configuration;
using ContactInfoHandler.Application.Core.Persons.Employees.Service;
using ContactInfoHandler.Application.Dto.Persons.Employees;
using ContactInfoHandler.Dominio.Core.Areas;
using ContactInfoHandler.Dominio.Core.Identifications;
using ContactInfoHandler.Dominio.Core.Persons;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace Test.ContactInfoHandler.Core._3._Application.Core.Employee
{
    public class EmployeeServiceTest
    {


        [Fact]
        [UnitTest]
        public async Task AlwaysNaturalKindOfPerson()
        {
            var EmployeeMock = new Mock<IEmployeeRepository>();
            IEnumerable<EmployeeEntity> empleados = new List<EmployeeEntity>();
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .Returns(() => Task.FromResult(empleados));

            var services = new ServiceCollection();
            services.AddTransient(_ => EmployeeMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var employeeService = provider.GetRequiredService<IEmployeeService>();
            var EmployeeToInsert = new EmployeeDto
            {
                DateOfBirth = DateTimeOffset.Now,
                SignUpDate = DateTimeOffset.Now,
                KindOfPerson = KindOfPerson.Juridica,
                KindOfIdentification = new KindOfIdentificationEntity { IdentificationName = "Cedula" }
            };
            var response = await employeeService.InsertEmployee(EmployeeToInsert).ConfigureAwait(false);

            Assert.Equal(KindOfPerson.Natural, EmployeeToInsert.KindOfPerson);
        }

        [Fact]
        [UnitTest]
        public async Task InsertFailKindAndNumberIdExistingOrSameNameAndKindOfPerson()
        {
            var EmployeeMock = new Mock<IEmployeeRepository>();
            var proovedores = new List<EmployeeEntity>();
            proovedores.Add(new EmployeeEntity { FirstName = "Any Name" });
            IEnumerable<EmployeeEntity> proovedoresEnum = proovedores;
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .Returns(() => Task.FromResult(proovedoresEnum));

            var services = new ServiceCollection();
            services.AddTransient(_ => EmployeeMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var EmployeeService = provider.GetRequiredService<IEmployeeService>();

            await Assert.ThrowsAsync<ArgumentException>(() => EmployeeService.InsertEmployee(new EmployeeDto { }));
        }

        [Fact]
        [UnitTest]
        public async Task InsertFailsKindOfIdIsNit()
        {
            var EmployeeMock = new Mock<IEmployeeRepository>();
            IEnumerable<EmployeeEntity> employees = new List<EmployeeEntity>();
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .Returns(() => Task.FromResult(employees));

            var services = new ServiceCollection();
            services.AddTransient(_ => EmployeeMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var EmployeeService = provider.GetRequiredService<IEmployeeService>();

            await Assert.ThrowsAsync<ArgumentException>(() => EmployeeService.InsertEmployee(new EmployeeDto { KindOfIdentification = new KindOfIdentificationEntity { IdentificationName = "NIT" } }));
        }

        [Fact]
        [UnitTest]
        public async Task InsertFailSingUpDateOrDateBirthNulls()
        {
            var EmployeeMock = new Mock<IEmployeeRepository>();
            IEnumerable<EmployeeEntity> employees = new List<EmployeeEntity>();
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .Returns(() => Task.FromResult(employees));

            var services = new ServiceCollection();
            services.AddTransient(_ => EmployeeMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var employeeService = provider.GetRequiredService<IEmployeeService>();

            await Assert.ThrowsAsync<ArgumentException>(() => employeeService.InsertEmployee(new EmployeeDto { }));
        }

        [Fact]
        [UnitTest]
        public async Task InsertSeccesfullPerson()
        {
            var EmployeeMock = new Mock<IEmployeeRepository>();
            IEnumerable<EmployeeEntity> empleados = new List<EmployeeEntity>();
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .Returns(() => Task.FromResult(empleados));

            var services = new ServiceCollection();
            services.AddTransient(_ => EmployeeMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var employeeService = provider.GetRequiredService<IEmployeeService>();

            var response = await employeeService.InsertEmployee(new EmployeeDto
            {
                DateOfBirth = DateTimeOffset.Now,
                SignUpDate = DateTimeOffset.Now,
                KindOfIdentification = new KindOfIdentificationEntity { IdentificationName = "Cedula" }
            }).ConfigureAwait(false);
            Assert.True(response);
        }

       
        [Fact]
        [IntegrationTest]
        public async Task SucessfullInsertProvider()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var EmployeeService = provider.GetRequiredService<IEmployeeService>();
            var KindOfIdentificationId = new Guid("d008599c-0ef9-4e91-8e5d-cd349b391253");
            var AreaId = Guid.NewGuid();
            var response = await EmployeeService.InsertEmployee(new EmployeeDto
            {
                AreaId = AreaId,
                AreaOfWork = new AreaOfWorkEntity { AreaId= AreaId, AreaName= "Finanzas"},
                Salary = 55.54564,
                EmployeeCode = Guid.NewGuid(),
                WorkPosition = WorkPosition.CommonEmployee,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = 1061822539,
                KindOfIdentificationId = KindOfIdentificationId,
                KindOfPerson = KindOfPerson.Juridica,
                KindOfIdentification = new KindOfIdentificationEntity { IdentificationName = "Cedula", KindOfIdentificationId = KindOfIdentificationId }
            }).ConfigureAwait(false);

            Assert.True(response);
        }

        [Fact]
        [IntegrationTest]
        public async Task FailInsertExistingIdAndKindOfId()
        {
            
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var ProvicerService = provider.GetRequiredService<IEmployeeService>();
            var KindOfIdentificationId = new Guid("d008599c-0ef9-4e91-8e5d-cd349b391253");
            var AreaId = Guid.NewGuid();
            await Assert.ThrowsAsync<ArgumentException>(() => ProvicerService.InsertEmployee(new EmployeeDto
            {
                AreaId = AreaId,
                AreaOfWork = new AreaOfWorkEntity { AreaId = AreaId, AreaName = "Finanzas" },
                Salary = 55.54564,
                EmployeeCode = Guid.NewGuid(),
                WorkPosition = WorkPosition.CommonEmployee,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = 1061822537,
                KindOfIdentificationId = KindOfIdentificationId,
                KindOfPerson = KindOfPerson.Juridica,
                KindOfIdentification = new KindOfIdentificationEntity { IdentificationName = "Cedula", KindOfIdentificationId = KindOfIdentificationId }
            }));
        }

        [Fact]
        [IntegrationTest]
        public async Task FailInsertNITDocument()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var EmployeeService = provider.GetRequiredService<IEmployeeService>();
            var KindOfIdentificationId = new Guid("d008599c-0ef9-4e91-8e5d-cd349b391263");
            var AreaId = Guid.NewGuid();
            await Assert.ThrowsAsync<ArgumentException>(() => EmployeeService.InsertEmployee(new EmployeeDto
            {
                AreaId = AreaId,
                AreaOfWork = new AreaOfWorkEntity { AreaId = AreaId, AreaName = "Finanzas" },
                Salary = 55.54564,
                EmployeeCode = Guid.NewGuid(),
                WorkPosition = WorkPosition.CommonEmployee,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = 1061822536,
                KindOfIdentificationId = KindOfIdentificationId,
                KindOfPerson = KindOfPerson.Juridica,
                KindOfIdentification = new KindOfIdentificationEntity { IdentificationName = "NIT", KindOfIdentificationId = KindOfIdentificationId }
            }));
        }

        [Fact]
        [IntegrationTest]
        public async Task FailNullDates()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var EmployeeService = provider.GetRequiredService<IEmployeeService>();
            var KindOfIdentificationId = new Guid("d008599c-0ef9-4e91-8e5d-cd349b391263");
            var AreaId = Guid.NewGuid();
            await Assert.ThrowsAsync<ArgumentException>(() => EmployeeService.InsertEmployee(new EmployeeDto
            {
                AreaId = AreaId,
                AreaOfWork = new AreaOfWorkEntity { AreaId = AreaId, AreaName = "Finanzas" },
                Salary = 55.54564,
                EmployeeCode = Guid.NewGuid(),
                WorkPosition = WorkPosition.CommonEmployee,
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                IdNumber = 1061822536,
                KindOfIdentificationId = KindOfIdentificationId,
                KindOfPerson = KindOfPerson.Juridica,
                KindOfIdentification = new KindOfIdentificationEntity { IdentificationName = "Cedula", KindOfIdentificationId = KindOfIdentificationId }
            }));
        }

        [Fact]
        [IntegrationTest]
        public async Task SuccesfullDeleteEmployee()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var employeeService = provider.GetRequiredService<IEmployeeService>();
            var IdEmployee = new Guid("BF54B3FB-0E73-4342-BFE3-08D8EFF6765E");
            var response = await employeeService.DeleteEmployee(new EmployeeDto
            {
                IdEmployee = IdEmployee,
            }).ConfigureAwait(false);

            Assert.True(response);
        }

    }
}
