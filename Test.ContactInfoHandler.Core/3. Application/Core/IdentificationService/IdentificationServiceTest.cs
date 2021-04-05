using ContactInfoHandler.Application.Core.Base.Exceptions;
using ContactInfoHandler.Application.Core.Identifications.Configuration;
using ContactInfoHandler.Application.Core.Identifications.Service;
using ContactInfoHandler.Application.Dto.Identifications;
using ContactInfoHandler.Dominio.Core.Identifications;
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

namespace Test.ContactInfoHandler.Core._3._Application.Core.IdentificationService
{
    public class IdentificationServiceTest
    {
        Guid KindOfIdId = Guid.NewGuid();

        #region UnitTests

        [Fact]
        [UnitTest]

        #region InsertTests
        public async Task InsertSeccesfullPerson()
        {
            var IdentificationRepoMock = new Mock<IKindOfIdentificationRepository>();
            IEnumerable<KindOfIdentificationEntity> kindOfIdentification = new List<KindOfIdentificationEntity>();
            IdentificationRepoMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
            .Returns(() => Task.FromResult(kindOfIdentification));

            var services = new ServiceCollection();
            services.AddTransient(_ => IdentificationRepoMock.Object);
            services.ConfigureKindOfIdentification(new DbSettings());
            var provider = services.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();

            var response = await KindOfIdService.InsertKindOfId(new KindOfIdentificationDto
            {
                KindOfIdentificationId = new Guid("D008599C-0EF9-4E91-8E5D-CD349B391153"),
                IdentificationName = "AnyName"
            }).ConfigureAwait(false);

            Assert.True(response);
        }

        [Fact]
        [UnitTest]
        public async Task InsertFaillPerson()
        {
            var IdentificationRepoMock = new Mock<IKindOfIdentificationRepository>();
            List<KindOfIdentificationEntity> kois = new List<KindOfIdentificationEntity>();
            kois.Add(new KindOfIdentificationEntity { IdentificationName = "Existing" });
            IEnumerable<KindOfIdentificationEntity> kindOfIdentification = kois;
            IdentificationRepoMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
            .Returns(() => Task.FromResult(kindOfIdentification));

            var services = new ServiceCollection();
            services.AddTransient(_ => IdentificationRepoMock.Object);
            services.ConfigureKindOfIdentification(new DbSettings());
            var provider = services.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();

            await Assert.ThrowsAsync<AlreadyExistingKindOfIDException>(() => KindOfIdService.InsertKindOfId(new KindOfIdentificationDto
            {
                KindOfIdentificationId = new Guid("D008599C-0EF9-4E91-8E5D-CD349B391153"),
                IdentificationName = "AnyName"
            }));


        }

        #endregion

        [Fact]
        [UnitTest]
        public async Task UpdateSuccesfull()
        {
            var IdentificationRepoMock = new Mock<IKindOfIdentificationRepository>();
            IdentificationRepoMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
            .Returns(() => Task.FromResult(new KindOfIdentificationEntity { IdentificationName = "Anyname" }));

            var services = new ServiceCollection();
            services.AddTransient(_ => IdentificationRepoMock.Object);
            services.ConfigureKindOfIdentification(new DbSettings());
            var provider = services.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();

            var response = await KindOfIdService.UpdateKindOfId(new KindOfIdentificationDto
            {
                KindOfIdentificationId = new Guid("D008599C-0EF9-4E91-8E5D-CD349B391153"),
                IdentificationName = "AnyName"
            }).ConfigureAwait(false);

            Assert.True(response);
        }

        [Fact]
        [UnitTest]
        public async Task DeleteSuccesull()
        {
            var IdentificationRepoMock = new Mock<IKindOfIdentificationRepository>();
            IdentificationRepoMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
            .Returns(() => Task.FromResult(new KindOfIdentificationEntity { IdentificationName = "Anyname" }));

            var services = new ServiceCollection();
            services.AddTransient(_ => IdentificationRepoMock.Object);
            services.ConfigureKindOfIdentification(new DbSettings());
            var provider = services.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();

            var response = await KindOfIdService.DeleteKindOfIdentification(new KindOfIdentificationDto
            {
                KindOfIdentificationId = new Guid("D008599C-0EF9-4E91-8E5D-CD349B391153"),
                IdentificationName = "AnyName"
            }).ConfigureAwait(false);

            Assert.True(response);
        }

        [Fact]
        [UnitTest]
        public async Task DeleteFailNoName()
        {

            var services = new ServiceCollection();
            services.ConfigureKindOfIdentification(new DbSettings());
            var provider = services.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();

            await Assert.ThrowsAsync<DeniedDeleteNameNullException>(() => KindOfIdService.DeleteKindOfIdentification(new KindOfIdentificationDto
            {
                KindOfIdentificationId = new Guid("D008599C-0EF9-4E91-8E5D-CD349B391153"),
            }));

        }


        #endregion

        #region IntegrationTests        

        #region GetTests        
        [Fact]
        [IntegrationTest]
        public async Task GetAllKindsOfId()
        {
            var service = new ServiceCollection();
            service.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();

            var AddedKindOfId = new KindOfIdentificationDto
            {
                KindOfIdentificationId = KindOfIdId,
                IdentificationName = "Registro Civil"
            };

            await KindOfIdService.InsertKindOfId(AddedKindOfId);

            var response = KindOfIdService.GetKindOfId().Result.Any();

            Assert.True(response);

            await KindOfIdService.DeleteKindOfIdentification(AddedKindOfId);
        }
        #endregion
        #region UpdateTests        
        [Fact]
        [IntegrationTest]
        public async Task SucessUpdate()
        {
            var service = new ServiceCollection();
            service.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();

            var AddedKindOfId = new KindOfIdentificationDto
            {
                KindOfIdentificationId = KindOfIdId,
                IdentificationName = "Registro Civil"
            };

            await KindOfIdService.InsertKindOfId(AddedKindOfId);

            var response = await KindOfIdService.UpdateKindOfId(new KindOfIdentificationDto
            {
                KindOfIdentificationId = KindOfIdId,
                IdentificationName = "Defuncion Civil"
            });
            Assert.True(response);

            await KindOfIdService.DeleteKindOfIdentification(AddedKindOfId);
        }

        [Fact]
        [IntegrationTest]
        public async Task FailUpdate()
        {
            var service = new ServiceCollection();
            service.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();

            await Assert.ThrowsAsync<noExistingKindOfIdException>(() => KindOfIdService.UpdateKindOfId(new KindOfIdentificationDto
            {
                KindOfIdentificationId = Guid.NewGuid(),
                IdentificationName = "Defuncion Civil"
            }));
        }
        #endregion
        #region InsertTests

        [Fact]
        [IntegrationTest]
        public async Task SuccesfullInsert()
        {
            var service = new ServiceCollection();
            service.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();
            var AnyGuid = Guid.NewGuid();
            var response = await KindOfIdService.InsertKindOfId(new KindOfIdentificationDto
            {
                KindOfIdentificationId = AnyGuid,
                IdentificationName = "AnyId"
            });
            Assert.True(response);

            await KindOfIdService.DeleteKindOfIdentification(new KindOfIdentificationDto
            {
                KindOfIdentificationId = AnyGuid,
                IdentificationName = "AnyId"
            });

        }
        #endregion
        #region DeleteTests

        [Fact]
        [IntegrationTest]
        public async Task DeleteDeniedKindOfIdCedulaImportant()
        {
            var service = new ServiceCollection();
            service.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();

            await Assert.ThrowsAsync<DeleteDeniedKindOfIdImportantException>(() => KindOfIdService.DeleteKindOfIdentification(new KindOfIdentificationDto
            {
                IdentificationName = "Cedula"
            }));
        }

        [Fact]
        [IntegrationTest]
        public async Task DeleteDeniedKindOfIdPasaporteImportant()
        {
            var service = new ServiceCollection();
            service.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();

            await Assert.ThrowsAsync<DeleteDeniedKindOfIdImportantException>(() => KindOfIdService.DeleteKindOfIdentification(new KindOfIdentificationDto
            {
                IdentificationName = "Pasaporte"
            }));
        }

        [Fact]
        [IntegrationTest]
        public async Task DeleteDeniedKindOfIdNitImportant()
        {
            var service = new ServiceCollection();
            service.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();

            await Assert.ThrowsAsync<DeleteDeniedKindOfIdImportantException>(() => KindOfIdService.DeleteKindOfIdentification(new KindOfIdentificationDto
            {
                IdentificationName = "Nit"
            }));
        }


        [Fact]
        [IntegrationTest]
        public async Task DeleteFailNoNameEspecified()
        {
            var service = new ServiceCollection();
            service.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();

            await Assert.ThrowsAsync<DeniedDeleteNameNullException>(() => KindOfIdService.DeleteKindOfIdentification(new KindOfIdentificationDto
            {
            }));
        }


        [Fact]
        [IntegrationTest]
        public async Task SuccesfulDetelete()
        {
            var service = new ServiceCollection();
            service.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var KindOfIdService = provider.GetRequiredService<IKindOfIdentificationService>();

            var addedKindOfId = new KindOfIdentificationDto { KindOfIdentificationId = Guid.NewGuid(), IdentificationName = "RegistroCivil" };

            await KindOfIdService.InsertKindOfId(addedKindOfId);

            var response = await KindOfIdService.DeleteKindOfIdentification(new KindOfIdentificationDto
            {
                KindOfIdentificationId = addedKindOfId.KindOfIdentificationId,
                IdentificationName = addedKindOfId.IdentificationName
            });

            Assert.True(response);
        }
        #endregion

        #endregion
    }
}