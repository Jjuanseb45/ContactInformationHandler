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
            IEnumerable<AreaOfWorkEntity> areas = new List<AreaOfWorkEntity>();
            areaMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<AreaOfWorkEntity, bool>>>()))
                .Returns(() => Task.FromResult(areas));

            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();

            Assert.False(AreaService.InsertArea(new AreaOfWorkDto {AreaName="Finanzas"}).Result);
        }

        [Fact]
        [UnitTest]
        public void InsertNoExistingArea()
        {

            var areaMock = new Mock<IAreaOfWorkRepository>();
            areaMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<AreaOfWorkEntity, bool>>>()))
                .Returns(()=>Task.FromResult(new AreaOfWorkEntity {}));

            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();

            Assert.True(AreaService.InsertArea(new AreaOfWorkDto { }).Result);
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
        /*
        [Fact]
        [UnitTest]
        public async Task CheckSuccesfullUpdateArea()
        {
            var areaMock = new Mock<IAreaOfWorkRepository>();
            areaMock.Setup(x => x.Update<AreaOfWorkEntity>(It.IsAny<AreaOfWorkEntity>()))
                .Returns(()=> Task.FromResult(true));
            var service = new ServiceCollection();
            service.AddTransient(_ => areaMock.Object);
            service.ConfigureAreaOfWork(new DbSettings());
            var provider = service.BuildServiceProvider();
            var AreaService = provider.GetRequiredService<IAreaOfWorkService>();
            await Assert.True(AreaService.UpdateArea(new AreaOfWorkDto { }).Result);
        } 
        */


    }
}
