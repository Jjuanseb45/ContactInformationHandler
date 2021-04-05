using ContactInfoHandler.Application.Core.Areas.Configuration;
using ContactInfoHandler.Application.Core.Areas.Service;
using ContactInfoHandler.Application.Core.Persons.Configuration;
using ContactInfoHandler.Application.Core.Persons.Employees.Service;
using ContactInfoHandler.Application.Dto.Areas;
using ContactInfoHandler.Application.Dto.Persons.Employees;
using ContactInfoHandler.Dominio.Core.Areas;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using ContactInfoHandler.Application.Core.Base.Exceptions;

namespace Test.ContactInfoHandler.Core._3._Application.Core.AreaService
{
    public class AreaServiceTests
    {

        #region Unit Tests

        [Fact]
        [UnitTest]
        public async Task FailInsertAreaNoResponsable()
        {

            var areaList = new List<AreaOfWorkEntity>();
            IEnumerable<AreaOfWorkEntity> areas = areaList;
            var areaMock = new Mock<IAreaOfWorkRepository>();
            areaMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<AreaOfWorkEntity, bool>>>())).Returns(() => Task.FromResult(areas));

            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();

            await Assert.ThrowsAsync<NoAreaHandlerEspecifiedException>(() => AreaService.InsertArea(new AreaOfWorkDto { }));

        }

        [Fact]
        [UnitTest]
        public void DeleteAreaWithEmployee()
        {

            var areaMock = new Mock<IAreaOfWorkRepository>();            
            areaMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<AreaOfWorkEntity, bool>>>()))
                .Returns(() => Task.FromResult(new AreaOfWorkEntity { AreaId = Guid.NewGuid(), AreaName="AnyName"}));

            var empleadosList = new List<EmployeeEntity>();
            empleadosList.Add(new EmployeeEntity { FirstName = "a" });

            IEnumerable<EmployeeEntity> empleados = empleadosList;
            var EmployeeMock = new Mock<IEmployeeRepository>();
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>())).Returns(() => Task.FromResult(empleados));

            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.AddTransient(_ => EmployeeMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();

            Assert.False(AreaService.DeleteArea(new AreaOfWorkDto { }).Result);
        }
        

        [Fact]
        [UnitTest]
        public void DeleteAreaWithoutEmployee()
        {

            var areaMock = new Mock<IAreaOfWorkRepository>();
            
            areaMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<AreaOfWorkEntity, bool>>>()))
                .Returns(() => Task.FromResult(new AreaOfWorkEntity { }));

            var empleadosList = new List<EmployeeEntity>();
            IEnumerable<EmployeeEntity> empleados = empleadosList;
            var EmployeeMock = new Mock<IEmployeeRepository>();
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>())).Returns(() => Task.FromResult(empleados));

            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.AddTransient(_ => EmployeeMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();

            Assert.True(AreaService.DeleteArea(new AreaOfWorkDto { }).Result);
        }
        

        [Fact]
        [UnitTest]
        public async Task InsertAlreadyExistingArea()
        {
            var areaMock = new Mock<IAreaOfWorkRepository>();

            List<AreaOfWorkEntity> areas = new List<AreaOfWorkEntity>();
            areas.Add(new AreaOfWorkEntity { AreaName = "AnyName" });
            IEnumerable<AreaOfWorkEntity> areasEnum = areas;
            areaMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<AreaOfWorkEntity, bool>>>()))
            .Returns(() => Task.FromResult(areasEnum));

            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();

            await Assert.ThrowsAsync<AlreadyExistingAreaException>(() => AreaService.InsertArea(new AreaOfWorkDto { AreaName = "Finanzas", ResponsableEmployeeId =Guid.NewGuid()}));

        }

        #endregion

        Guid AreaId = new Guid("76327fe9-afbd-42d9-b1ab-240b81be7e45");

        #region IntegrationTests        

        #region UpdateTests

        [Fact]
        [IntegrationTest]
        public async Task SucessfullUpdate() 
        {

            var serviceCollector = new ServiceCollection();
            serviceCollector.ConfigureAreaOfWork(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var serviceProvider = serviceCollector.BuildServiceProvider();
            var Service = serviceProvider.GetRequiredService<IAreaOfWorkService>();

            var addedArea = new AreaOfWorkDto
            {
                AreaId = AreaId,
                AreaName = "Secretaria",
                ResponsableEmployeeId = new Guid("ba841a9c-b6ab-4a9c-b9bb-effba4dd535d")
            };

            await Service.InsertArea(addedArea);

            var AreaEdited = new AreaOfWorkDto { AreaId = AreaId, AreaName = "Papeleria" };

            var response = await Service.UpdateArea(AreaEdited).ConfigureAwait(false);
            Assert.True(response);

            await Service.DeleteArea(addedArea);
        }
        
        [Fact]
        [IntegrationTest]
        public async Task FailUpdateAreaNoExisting() 
        {

            var serviceCollector = new ServiceCollection();
            serviceCollector.ConfigureAreaOfWork(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var serviceProvider = serviceCollector.BuildServiceProvider();
            var Service = serviceProvider.GetRequiredService<IAreaOfWorkService>();

            var AreaEdited = new AreaOfWorkDto { AreaId = Guid.NewGuid(), AreaName = "Papeleria" };

            var response = await Service.UpdateArea(AreaEdited).ConfigureAwait(false);
            Assert.False(response);
        }



        #endregion

        #region InsertTests        
        [Fact]
        [IntegrationTest]    
        public async Task SuccesfullInsert()
        {
            var service = new ServiceCollection();
            var EmployeeServiceColector = new ServiceCollection();

            service.ConfigureAreaOfWork(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            EmployeeServiceColector.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();
            var EmployeeProvider = EmployeeServiceColector.BuildServiceProvider();

            var AreaOfWorkService = provider.GetRequiredService<IAreaOfWorkService>();
            var EmployeeService = EmployeeProvider.GetRequiredService<IEmployeeService>();


            var AreaAdded = new AreaOfWorkDto
            {
                AreaId = AreaId,
                AreaName = "Ventas",
                ResponsableEmployeeId = Guid.NewGuid()
            };

            var response = await AreaOfWorkService.InsertArea(AreaAdded);
            Assert.True(response);

            await AreaOfWorkService.DeleteArea(AreaAdded).ConfigureAwait(false);

        }


        [Fact]
        [IntegrationTest]
        public async Task FailInsertNoResponsable()
        {
            var service = new ServiceCollection();
            service.ConfigureAreaOfWork(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var AreaOfWorkService = provider.GetRequiredService<IAreaOfWorkService>();
            await Assert.ThrowsAsync<NoAreaHandlerEspecifiedException>(() => AreaOfWorkService.InsertArea(new AreaOfWorkDto
            {
                AreaId = AreaId,
                AreaName = "Economia",
            }));
        }      

        [Fact]
        [IntegrationTest]    
        public async Task FailAlreadyExistingAreaInsert()
        {
            var service = new ServiceCollection();
            var EmployeeServiceColector = new ServiceCollection();

            service.ConfigureAreaOfWork(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            EmployeeServiceColector.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();
            var EmployeeProvider = EmployeeServiceColector.BuildServiceProvider();

            var AreaOfWorkService = provider.GetRequiredService<IAreaOfWorkService>();
            

            var AreaAdded = new AreaOfWorkDto
            {
                AreaId = AreaId,
                AreaName = "Economia",
                ResponsableEmployeeId = Guid.NewGuid()
            };

            await AreaOfWorkService.InsertArea(AreaAdded);

            var DuplicateArea = new AreaOfWorkDto
            {
                AreaId = AreaId,
                ResponsableEmployeeId = Guid.NewGuid()
            };

            await Assert.ThrowsAsync<AlreadyExistingAreaException>(() => AreaOfWorkService.InsertArea(DuplicateArea));

            await AreaOfWorkService.DeleteArea(AreaAdded);
        }
        #endregion

        #region DeleteTests

        [Fact]
        [IntegrationTest]
        public async Task SuccesfulDetelete()
        {
            var service = new ServiceCollection();
            service.ConfigureAreaOfWork(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();

            var AreaOfWorkService = provider.GetRequiredService<IAreaOfWorkService>();
            var ReponsableId = new Guid("dc29cca5-e978-4f54-a177-5b935820e9f7");

            var AreaToDelete = new AreaOfWorkDto {
                AreaId = AreaId,
                ResponsableEmployeeId = ReponsableId,
                AreaName = "Finanzas",
            };
            await AreaOfWorkService.InsertArea(AreaToDelete);

            var response = await AreaOfWorkService.DeleteArea(new AreaOfWorkDto 
            {
                AreaId = AreaId,
                ResponsableEmployeeId = ReponsableId,
                AreaName = "Finanzas",
            }
            );

            Assert.True(response);
        }

        [Fact]
        [IntegrationTest]
        public async Task FailDeleteAreaHasEmployees()
        {
            var service = new ServiceCollection();
            var EmployeeServiceCollector = new ServiceCollection();

            service.ConfigureAreaOfWork(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            EmployeeServiceCollector.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();
            var employeeProvider = EmployeeServiceCollector.BuildServiceProvider();

            var AreaOfWorkService = provider.GetRequiredService<IAreaOfWorkService>();
            var EmployeeService = employeeProvider.GetRequiredService<IEmployeeService>();

            var EmployeeOnArea = new EmployeeDto
            {
                AreaId = AreaId,
                DateOfBirth = DateTimeOffset.Now,
                SignUpDate = DateTimeOffset.Now,
                IdEmployee = Guid.NewGuid(),
                EmployeeCode = Guid.NewGuid(),
                KindOfIdentificationId = Guid.NewGuid(),
                IdNumber = 256486465,
                KindOfPerson="Natural"
            };

            await EmployeeService.InsertEmployee(EmployeeOnArea);

            var ReponsableId = new Guid("dc29cca5-e978-4f54-a177-5b935820e9f7");

            var AreaToDelete = new AreaOfWorkDto
            {
                AreaId = AreaId,
                ResponsableEmployeeId = ReponsableId,
                AreaName = "Finanzas",
            };

            var response = await AreaOfWorkService.DeleteArea(AreaToDelete);            

            Assert.False(response);
            await EmployeeService.DeleteEmployee(EmployeeOnArea);
        }

        [Fact]
        [IntegrationTest]

        public async Task FailDeleteNoExistingArea() 
        {
            var service = new ServiceCollection();
            service.ConfigureAreaOfWork(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();

            var AreaOfWorkService = provider.GetRequiredService<IAreaOfWorkService>();
            var ReponsableId = new Guid("dc29cca5-e978-4f54-a177-5b935820e9f7");

            var AreaToDelete = new AreaOfWorkDto
            {
                AreaId = AreaId,
                ResponsableEmployeeId = ReponsableId,
                AreaName = "Finanzas",
            };

            var response = await AreaOfWorkService.DeleteArea(AreaToDelete);

            Assert.False(response);
        }

        #endregion

        #endregion

        /*
         
        //TODO: COMO HAGO QUE ME RETORNE NULL EL MOCK
        [Fact]
        [UnitTest]
        public void SucesfullInsertNoExistingArea()
        {

            var areaMock = new Mock<IAreaOfWorkRepository>();
            var areas = new List<AreaOfWorkEntity>();
            IEnumerable<AreaOfWorkEntity> areasEnum = areas;
            areaMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<AreaOfWorkEntity, bool>>>()));
                //.Returns(()=>Task.FromResult(null));

            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();

            Assert.True(AreaService.InsertArea(new AreaOfWorkDto { ResponsableEmployeeId = Guid.NewGuid()}).Result);
        }

        [Fact]
        [UnitTest]
        public async Task SuccesfullUpdateArea()
        {
            var areaMock = new Mock<IAreaOfWorkRepository>();
            areaMock.Setup(x => x.GetOne<AreaOfWorkEntity>(It.IsAny<Expression<Func<AreaOfWorkEntity, bool>>>()))
                .Returns(()=> Task.FromResult(new AreaOfWorkEntity { }));

            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();
            Assert.True(AreaService.UpdateArea(new AreaOfWorkDto {AreaName="A" }, new Guid()).Result);
        }

        [Fact]
        [UnitTest]
        public async Task CheckNoExistingAreas()
        {
            var areaMock = new Mock<IAreaOfWorkRepository>();
            IEnumerable<AreaOfWorkEntity> lista = new List<AreaOfWorkEntity>();
            areaMock.Setup(x => x.GetAll<AreaOfWorkEntity>())
                .Returns(() => Task.FromResult(lista));

            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();
            await Assert.ThrowsAsync<ArgumentException>(() => AreaService.GetAreas());
        }
        */
    }
}
