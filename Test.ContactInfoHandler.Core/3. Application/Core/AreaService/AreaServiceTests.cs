using ContactInfoHandler.Application.Core.Areas.Configuration;
using ContactInfoHandler.Application.Core.Areas.Service;
using ContactInfoHandler.Application.Dto.Areas;
using ContactInfoHandler.Dominio.Core.Areas;
using ContactInfoHandler.Dominio.Core.Persons.Employees;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace Test.ContactInfoHandler.Core._3._Application.Core.AreaService
{
    public class AreaServiceTests
    {
        [Fact]
        [UnitTest]
        public void DeleteAreaWithEmployee()
        {

            var areaMock = new Mock<IAreaOfWorkRepository>();
            ICollection<EmployeeEntity> empleados = new List<EmployeeEntity>();
            empleados.Add (new EmployeeEntity { FirstName= "a"});
            areaMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<AreaOfWorkEntity, bool>>>()))
                .Returns(() => Task.FromResult(new AreaOfWorkEntity { AreaEmployees = empleados }));

            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
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
            ICollection<EmployeeEntity> empleados = new List<EmployeeEntity>();
            areaMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<AreaOfWorkEntity, bool>>>()))
                .Returns(() => Task.FromResult(new AreaOfWorkEntity { AreaEmployees = empleados }));

            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();

            Assert.True(AreaService.DeleteArea(new AreaOfWorkDto { }).Result);
        }

        [Fact]
        [UnitTest]
        public void InsertAlreadyExistingArea()
        {
            var areaMock = new Mock<IAreaOfWorkRepository>();

            var areas = new List<AreaOfWorkEntity>();
            areas.Add(new AreaOfWorkEntity { AreaName = "Any Name" });
            IEnumerable<AreaOfWorkEntity> areasEnum = areas;
            areaMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<AreaOfWorkEntity, bool>>>()))
            .Returns(() => Task.FromResult(areasEnum));
            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();

            Assert.False(AreaService.InsertArea(new AreaOfWorkDto {AreaName="Finanzas"}).Result);
        }

        [Fact]
        [UnitTest]
        public void SucesfullInsertNoExistingArea()
        {

            var areaMock = new Mock<IAreaOfWorkRepository>();
            var areas = new List<AreaOfWorkEntity>();
            IEnumerable<AreaOfWorkEntity> areasEnum = areas;
            areaMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<AreaOfWorkEntity, bool>>>()))
                .Returns(()=>Task.FromResult(areasEnum));

            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();

            Assert.True(AreaService.InsertArea(new AreaOfWorkDto { ResponsableEmployeeId = new Guid()}).Result);
        }

        [Fact]
        [UnitTest]
        public async Task CheckNoExistingAreas()
        {
            var areaMock = new Mock<IAreaOfWorkRepository>();
            IEnumerable <AreaOfWorkEntity> lista = new List<AreaOfWorkEntity>();
            areaMock.Setup(x => x.GetAll<AreaOfWorkEntity>())
                .Returns(()=> Task.FromResult(lista));

            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();
            await Assert.ThrowsAsync<ArgumentException>(() => AreaService.GetAreas());
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
        [IntegrationTest]    
        public async Task SuccesfullInsert()
        {
            var service = new ServiceCollection();
            service.ConfigureAreaOfWork(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var AreaOfWorkService = provider.GetRequiredService<IAreaOfWorkService>();
            var ReponsableId = new Guid("dc29cca5-e978-4f54-a177-5b935820e9f7");
            var AreaId = new Guid("DBFE49F6-BB24-4B75-4516-08D8F01092A6");
            var response = await AreaOfWorkService.InsertArea(new AreaOfWorkDto
            {
                AreaId = AreaId,
                AreaName= "Economia",
                ResponsableEmployeeId = ReponsableId,
            });
            Assert.True(response);
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
            var ReponsableId = new Guid("dc29cca5-e978-4f54-a177-5b935820e9f7");
            var AreaId = new Guid("a720d6b6-d84f-476e-a95a-754f9ba078b9");
            var response = await AreaOfWorkService.InsertArea(new AreaOfWorkDto
            {
                AreaId = AreaId,
                AreaName = "Economia",
            });
            Assert.False(response);
        }      

        [Fact]
        [IntegrationTest]    
        public async Task FailAlreadyExistingAreafullInsert()
        {
            var service = new ServiceCollection();
            service.ConfigureAreaOfWork(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var AreaOfWorkService = provider.GetRequiredService<IAreaOfWorkService>();
            var ReponsableId = new Guid("dc29cca5-e978-4f54-a177-5b935820e9f7");
            var AreaId = new Guid("DBFE49F6-BB24-4B75-4516-08D8F01092E1");
            var response = await AreaOfWorkService.InsertArea(new AreaOfWorkDto
            {
                AreaId = AreaId,
                AreaName= "Finanzas",
                ResponsableEmployeeId = ReponsableId,
            });
            Assert.False(response);
        }

        /*
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
            var AreaId = new Guid("DBFE49F6-BB24-4B75-4516-08D8F01092E1");
            var response = await AreaOfWorkService.DeleteArea(new AreaOfWorkDto
            {
                AreaId = AreaId,
                ResponsableEmployeeId = ReponsableId,
                Reponsable = new EmployeeEntity { AreaId = AreaId },
                AreaName = "Finanzas",
                
            });
            Assert.True(response);
        }


        */

    }
}
