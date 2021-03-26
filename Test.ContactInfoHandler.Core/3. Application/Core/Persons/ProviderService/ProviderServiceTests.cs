using ContactInfoHandler.Application.Core.Persons.Configuration;
using ContactInfoHandler.Application.Core.Persons.Providers.Service;
using ContactInfoHandler.Application.Dto.Persons.Providers;
using ContactInfoHandler.Dominio.Core.Identifications;
using ContactInfoHandler.Dominio.Core.Persons;
using ContactInfoHandler.Dominio.Core.Persons.Providers;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace Test.ContactInfoHandler.Core._3._Application.Core.Persons.ProviderService
{
    public class ProviderServiceTests
    {
        [Fact]
        [UnitTest]
        public async Task InsertFailKindAndNumberIdExistingOrSameNameAndKindOfPerson()
        {
            var ProviderMock = new Mock<IProviderRepository>();
            var proovedores = new List<ProviderEntity>() ;
            proovedores.Add(new ProviderEntity {FirstName="Any Name" });
            IEnumerable <ProviderEntity> proovedoresEnum = proovedores;
            ProviderMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<ProviderEntity, bool>>>()))
            .Returns(() => Task.FromResult(proovedoresEnum));          

            var services = new ServiceCollection();
            services.AddTransient(_ => ProviderMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var ProviderService = provider.GetRequiredService<IProviderService>();

            await Assert.ThrowsAsync<ArgumentException>(() => ProviderService.InsertProvider(new ProviderDto { }));
        }
        
        [Fact]
        [UnitTest]
        public async Task InsertFailSingUpDateOrDateBirthNulls()
        {
            var ProviderMock = new Mock<IProviderRepository>();            
            IEnumerable <ProviderEntity> proveedores = new List<ProviderEntity>();
            ProviderMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<ProviderEntity, bool>>>()))
            .Returns(() => Task.FromResult(proveedores));          

            var services = new ServiceCollection();
            services.AddTransient(_ => ProviderMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var ProviderService = provider.GetRequiredService<IProviderService>();

            await Assert.ThrowsAsync<ArgumentException>(() => ProviderService.InsertProvider(new ProviderDto { }));
        }

        [Fact]
        [UnitTest]
        public async Task InsertFailsKindOfIdIsNit()
        {
            var ProviderMock = new Mock<IProviderRepository>();
            IEnumerable<ProviderEntity> proveedores = new List<ProviderEntity>();
            ProviderMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<ProviderEntity, bool>>>()))
            .Returns(() => Task.FromResult(proveedores));

            var services = new ServiceCollection();
            services.AddTransient(_ => ProviderMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var ProviderService = provider.GetRequiredService<IProviderService>();

            await Assert.ThrowsAsync<ArgumentException>(() => ProviderService.InsertProvider(new ProviderDto { KindOfIdentification = new KindOfIdentificationEntity { IdentificationName="NIT"} }));
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
            var ProvicerService = provider.GetRequiredService<IProviderService>();
            var KindOfIdentificationId = new Guid("d008599c-0ef9-4e91-8e5d-cd349b491263");
            var response = await ProvicerService.InsertProvider(new ProviderDto
            {
                CompanyName="Coopservir",
                ContactNumber= 3216588478,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName="Andres",
                SecondName= "Manuel",
                FirstLastName="Castro",
                SecondLastName = "Lopez",
                SignUpDate =DateTimeOffset.Now,
                IdNumber = 22554215,
                KindOfIdentificationId = KindOfIdentificationId,
                KindOfPerson = KindOfPerson.Juridica,
                KindOfIdentification = new KindOfIdentificationEntity { IdentificationName = "Cedula", KindOfIdentificationId = KindOfIdentificationId }
            }).ConfigureAwait(false);

            Assert.True(response);
        }
            
        [Fact]
        [IntegrationTest]
        public async Task FailInsertProviderSameIdNumberAndKindOfId() 
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var ProvicerService = provider.GetRequiredService<IProviderService>();
            var KindOfIdentificationId = new Guid("d008599c-0ef9-4e91-8e5d-cd349b391253");                       
            await Assert.ThrowsAsync<ArgumentException>(() => ProvicerService.InsertProvider(new ProviderDto
            {
               
                IdNumber = 2254215,
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
            var ProvicerService = provider.GetRequiredService<IProviderService>();
            var KindOfIdentificationId = new Guid("d008599c-0ef9-4e91-8e5d-cd349b391263");                       
            await Assert.ThrowsAsync<ArgumentException>(() => ProvicerService.InsertProvider(new ProviderDto
            {
                CompanyName = "Coopservir",
                ContactNumber = 3216588478,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Luis",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = 2254215,
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
            var ProvicerService = provider.GetRequiredService<IProviderService>();
            var KindOfIdentificationId = new Guid("d008599c-0ef9-4e91-8e5d-cd349b391263");                       
            await Assert.ThrowsAsync<ArgumentException>(() => ProvicerService.InsertProvider(new ProviderDto
            {
                CompanyName = "Coopservir",
                ContactNumber = 3216588478,
                FirstName = "Pedro",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",               
                IdNumber = 2254215,
                KindOfIdentificationId = KindOfIdentificationId,
                KindOfPerson = KindOfPerson.Juridica,
                KindOfIdentification = new KindOfIdentificationEntity { IdentificationName = "Pasaporte", KindOfIdentificationId = KindOfIdentificationId }
            }));

        }


        [Fact]
        [IntegrationTest]
        public async Task SuccesfullDeleteProvider()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var ProvicerService = provider.GetRequiredService<IProviderService>();
            var IdProvider = new Guid("2B1E873C-5031-4A0E-AFF6-7F20DF989F13");
            var response = await ProvicerService.DeleteProvider(new ProviderDto
            {
                IdProvider = IdProvider,               
            }).ConfigureAwait(false);

            Assert.True(response);
        }
    }
}
