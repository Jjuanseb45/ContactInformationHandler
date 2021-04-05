using ContactInfoHandler.Application.Core.Base.Exceptions;
using ContactInfoHandler.Application.Core.Identifications.Configuration;
using ContactInfoHandler.Application.Core.Identifications.Service;
using ContactInfoHandler.Application.Core.Persons.Configuration;
using ContactInfoHandler.Application.Core.Persons.Providers.Service;
using ContactInfoHandler.Application.Dto.Identifications;
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
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace Test.ContactInfoHandler.Core._3._Application.Core.Persons.ProviderService
{
    public class ProviderServiceTests
    {

        [Fact]
        [UnitTest]
        public async Task InsertFailNoKindOfPersonEspecified()
        {

            var services = new ServiceCollection();

            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var ProviderService = provider.GetRequiredService<IProviderService>();

            await Assert.ThrowsAsync<InvalidKindOfPerson>(() => ProviderService.InsertProvider(new ProviderDto { }));
        }

        #region Unit Tests        
        [Fact]
        [UnitTest]
        public async Task InsertFailKindAndNumberIdExistingOrSameNameAndKindOfPerson()
        {
            var ProviderMock = new Mock<IProviderRepository>();
            var proovedores = new List<ProviderEntity>();
            proovedores.Add(new ProviderEntity { FirstName = "Any Name" });
            IEnumerable<ProviderEntity> proovedoresEnum = proovedores;
            ProviderMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<ProviderEntity, bool>>>()))
            .Returns(() => Task.FromResult(proovedoresEnum));

            var KindOfIdMock = new Mock<IKindOfIdentificationRepository>();
            KindOfIdMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
                .Returns(() => Task.FromResult(new KindOfIdentificationEntity
                {
                    IdentificationName = "Any",
                    KindOfIdentificationId = Guid.NewGuid()
                }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => ProviderMock.Object);
            services.AddTransient(_ => KindOfIdMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var ProviderService = provider.GetRequiredService<IProviderService>();

            await Assert.ThrowsAsync<PersonWithSameParametersExistingException>(() => ProviderService.InsertProvider(new ProviderDto { KindOfPerson = "Juridica" }));
        }

        [Fact]
        [UnitTest]
        public async Task InsertFailSingUpDateNull()
        {
            var ProviderMock = new Mock<IProviderRepository>();
            IEnumerable<ProviderEntity> proveedores = new List<ProviderEntity>();
            ProviderMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<ProviderEntity, bool>>>()))
            .Returns(() => Task.FromResult(proveedores));

            var KindOfIdMock = new Mock<IKindOfIdentificationRepository>();
            KindOfIdMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
                .Returns(() => Task.FromResult(new KindOfIdentificationEntity
                {
                    IdentificationName = "Any",
                    KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
                }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => ProviderMock.Object);
            services.AddTransient(_ => KindOfIdMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var ProviderService = provider.GetRequiredService<IProviderService>();

            await Assert.ThrowsAsync<SignUpDateMissingException>(() => ProviderService.InsertProvider(new ProviderDto
            {

                CompanyName = "Margarita",
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
                IdProvider = Guid.NewGuid(),
                DateOfBirth = DateTimeOffset.Now,
                KindOfPerson = "Juridica"
            }));
        }

        [Fact]
        [UnitTest]
        public async Task InsertFailBirthDateNull()
        {
            var ProviderMock = new Mock<IProviderRepository>();
            IEnumerable<ProviderEntity> proveedores = new List<ProviderEntity>();
            ProviderMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<ProviderEntity, bool>>>()))
            .Returns(() => Task.FromResult(proveedores));

            var KindOfIdMock = new Mock<IKindOfIdentificationRepository>();
            KindOfIdMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
                .Returns(() => Task.FromResult(new KindOfIdentificationEntity
                {
                    IdentificationName = "Any",
                    KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
                }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => ProviderMock.Object);
            services.AddTransient(_ => KindOfIdMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var ProviderService = provider.GetRequiredService<IProviderService>();

            await Assert.ThrowsAsync<DateOfBirthMissingException>(() => ProviderService.InsertProvider(new ProviderDto
            {

                CompanyName = "Margarita",
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
                IdProvider = Guid.NewGuid(),
                SignUpDate = DateTimeOffset.Now,
                KindOfPerson = "Juridica"
            }));
        }

        [Fact]
        [UnitTest]
        public async Task InsertFailsKindOfIdIsNit()
        {
            var ProviderMock = new Mock<IProviderRepository>();
            IEnumerable<ProviderEntity> proveedores = new List<ProviderEntity>();
            ProviderMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<ProviderEntity, bool>>>()))
            .Returns(() => Task.FromResult(proveedores));

            var KindOfIdMock = new Mock<IKindOfIdentificationRepository>();
            KindOfIdMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
                .Returns(() => Task.FromResult(new KindOfIdentificationEntity
                {
                    IdentificationName = "Any",
                    KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6"),
                }
            ));


            var services = new ServiceCollection();
            services.AddTransient(_ => ProviderMock.Object);
            services.AddTransient(_ => KindOfIdMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var ProviderService = provider.GetRequiredService<IProviderService>();

            await Assert.ThrowsAsync<EmployeeWithNitException>(() => ProviderService.InsertProvider(new ProviderDto
            {
                DateOfBirth = DateTimeOffset.Now,
                SignUpDate = DateTimeOffset.Now,
                KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6"),
                KindOfPerson = "Juridica"
            }));
        }

        [Fact]
        [UnitTest]
        public async Task SucesfullInsertProvider()
        {
            var ProviderMock = new Mock<IProviderRepository>();
            IEnumerable<ProviderEntity> proveedores = new List<ProviderEntity>();
            ProviderMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<ProviderEntity, bool>>>()))
                .Returns(() => Task.FromResult(proveedores));

            var KindOfIdMock = new Mock<IKindOfIdentificationRepository>();
            KindOfIdMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
                    .Returns(() => Task.FromResult(new KindOfIdentificationEntity
                    {
                        IdentificationName = "Any",
                        KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
                    }
                ));


            var services = new ServiceCollection();
            services.AddTransient(_ => ProviderMock.Object);
            services.AddTransient(_ => KindOfIdMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var ProviderService = provider.GetRequiredService<IProviderService>();

            var response = await ProviderService.InsertProvider(new ProviderDto
            {
                IdProvider = ProviderIdentification,
                CompanyName = "Coopservir",
                ContactNumber = 3216588478,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = new Guid("D008599C-0EF9-4E91-8E5D-CD349B391153"),
                KindOfPerson = "Juridica",
            });

            Assert.True(response);

        }


        long IdNumber = 22554215;
        Guid ProviderIdentification = new Guid("d008592c-0ef9-4e91-8e5d-cd349b491263");
        #endregion

        #region Integration Tests

        #region InsertTests

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
           
            var AddedProvider = new ProviderDto
            {
                IdProvider = ProviderIdentification,
                CompanyName = "Coopservir",
                ContactNumber = 3216588478,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Juridica",
            };

            var response = await ProvicerService.InsertProvider(AddedProvider).ConfigureAwait(false);

            Assert.True(response);
            await ProvicerService.DeleteProvider(AddedProvider).ConfigureAwait(false);
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

            var KindOfIdentificationId = Guid.NewGuid();
       
            var provider = service.BuildServiceProvider();

            var ProvicerService = provider.GetRequiredService<IProviderService>();
            
            var AddedProvider = new ProviderDto
            {
                KindOfPerson="Natural",
                CompanyName = "Custer",
                IdNumber = IdNumber,
                KindOfIdentificationId = KindOfIdentificationId,
                IdProvider = Guid.NewGuid(),
                DateOfBirth = DateTimeOffset.Now,
                SignUpDate = DateTimeOffset.Now
            };
            await ProvicerService.InsertProvider(AddedProvider);

            await Assert.ThrowsAsync<PersonWithSameParametersExistingException>(() => ProvicerService.InsertProvider(new ProviderDto
            {
                IdNumber = IdNumber,
                FirstLastName = "Sal",
                CompanyName = "Custer",
                KindOfIdentificationId = KindOfIdentificationId,
                DateOfBirth = DateTimeOffset.Now,
                SignUpDate = DateTimeOffset.Now,
                IdProvider = Guid.NewGuid(),
                KindOfPerson = "Natural"
            }));

            await ProvicerService.DeleteProvider(AddedProvider);
        }

        [Fact]
        [IntegrationTest]
        public async Task FailInsertExistingNameAndKindOfPerson()
        {

            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });


            var Employeeprovider = service.BuildServiceProvider();

            var ProviderService = Employeeprovider.GetRequiredService<IProviderService>();          

            var AddedProvider = new ProviderDto
            {
                IdProvider= Guid.NewGuid(),       
                CompanyName = "AnyName",
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Natural",
            };           

            var response = await ProviderService.InsertProvider(AddedProvider).ConfigureAwait(false);

            await Assert.ThrowsAsync<PersonWithSameParametersExistingException>(() => ProviderService.InsertProvider(new ProviderDto
            {
                CompanyName = "AnyName",
                IdProvider = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Natural"
            }));

            await ProviderService.DeleteProvider(AddedProvider).ConfigureAwait(false);

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
            var KindOfIdServiceCollector = new ServiceCollection();
            KindOfIdServiceCollector.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();
            var KindOfIdProvider = KindOfIdServiceCollector.BuildServiceProvider();

            var ProvicerService = provider.GetRequiredService<IProviderService>();
            var kindOfIdService = KindOfIdProvider.GetRequiredService<IKindOfIdentificationService>();

            var verifyNitOnDb = kindOfIdService.VerifyExisting(new KindOfIdentificationDto { IdentificationName = "Nit" });

            string koi = "Nit";

            if (verifyNitOnDb.Result)
            {
                var NitEntityA = await kindOfIdService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);

                await Assert.ThrowsAsync<EmployeeWithNitException>(() => ProvicerService.InsertProvider(new ProviderDto
                {
                    CompanyName = "Coopservir",
                    ContactNumber = 3216588478,
                    DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                    FirstName = "Luis",
                    SecondName = "Manuel",
                    FirstLastName = "Castro",
                    SecondLastName = "Lopez",
                    SignUpDate = DateTimeOffset.Now,
                    IdNumber = IdNumber,
                    KindOfIdentificationId = NitEntityA.KindOfIdentificationId,
                    KindOfPerson = "Juridica",
                }));
            }
            else {
            await kindOfIdService.InsertKindOfId(new KindOfIdentificationDto { IdentificationName = "Nit", KindOfIdentificationId = Guid.NewGuid() });
            var NitEntityB = await kindOfIdService.GetOne(new KindOfIdentificationDto { IdentificationName = "Nit" }).ConfigureAwait(false);

            await Assert.ThrowsAsync<EmployeeWithNitException>(() => ProvicerService.InsertProvider(new ProviderDto
            {
                CompanyName = "Coopservir",
                ContactNumber = 3216588478,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Luis",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = NitEntityB.KindOfIdentificationId,
                KindOfPerson = "Juridica",
            }));
            }
        }

        [Fact]
        [IntegrationTest]
        public async Task FailBirthDateNull()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var ProvicerService = provider.GetRequiredService<IProviderService>();
            await Assert.ThrowsAsync<DateOfBirthMissingException>(() => ProvicerService.InsertProvider(new ProviderDto
            {
                CompanyName = "Coopservir",
                SignUpDate = DateTimeOffset.Now,
                ContactNumber = 3216588478,
                FirstName = "Pedro",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Juridica",
            }));
        }

        [Fact]
        [IntegrationTest]
        public async Task FailSignUpDateNull()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var ProvicerService = provider.GetRequiredService<IProviderService>();
            await Assert.ThrowsAsync<SignUpDateMissingException>(() => ProvicerService.InsertProvider(new ProviderDto
            {
                CompanyName = "Coopservir",
                DateOfBirth = DateTimeOffset.Now,
                ContactNumber = 3216588478,
                FirstName = "Pedro",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Juridica",
            }));
        }

        #endregion

        #region DeleteTests

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


            var AddedProvider = new ProviderDto
            {
                IdProvider = ProviderIdentification,
                CompanyName = "Coopservir",
                ContactNumber = 3216588478,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Juridica",
            };

            await ProvicerService.InsertProvider(AddedProvider);

            var response = await ProvicerService.DeleteProvider(AddedProvider).ConfigureAwait(false);

            Assert.True(response);
        }


        [Fact]
        [IntegrationTest]
        public async Task FailDeleteProviderNoExisting()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();

            var ProvicerService = provider.GetRequiredService<IProviderService>();

            var ProviderToDelete = new ProviderDto
            {
                IdProvider = ProviderIdentification,
                CompanyName = "Coopservir",
                ContactNumber = 3216588478,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Juridica",
            };

            await Assert.ThrowsAsync<NoExistingProviderException>(() => ProvicerService.DeleteProvider(ProviderToDelete));

        }
        #endregion

        #region UpdateTests
        [Fact]
        [IntegrationTest]
        public async Task SuccesfullProviderUpdate()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });


            var provider = service.BuildServiceProvider();

            var providerService = provider.GetRequiredService<IProviderService>();         

            var IdProviderToEdit = Guid.NewGuid();

            var AddedProvider = new ProviderDto
            {
                CompanyName="Koala",
                ContactNumber= 3219655215,
                IdProvider= IdProviderToEdit,
                KindOfPerson = "Juridica",
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
            };

            await providerService.InsertProvider(AddedProvider);

            var response = await providerService.UpdateProvider(new ProviderDto
            {
                IdProvider = IdProviderToEdit,
                KindOfPerson = "Natural",                
                SignUpDate = DateTimeOffset.Now,
                DateOfBirth = DateTimeOffset.Now
            });

            Assert.True(response);

            await providerService.DeleteProvider(AddedProvider);
        }

        [Fact]
        [IntegrationTest]
        public async Task UpdateFailProviderDoesntExist()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();

            var ProviderService = provider.GetRequiredService<IProviderService>();

            await Assert.ThrowsAsync<NoExistingProviderException>(() => ProviderService.UpdateProvider(new ProviderDto
            {
                IdProvider = Guid.NewGuid(),
                KindOfPerson = "Natural",
                SignUpDate = DateTimeOffset.Now,
                DateOfBirth = DateTimeOffset.Now
            }));


        }

        #endregion

        #endregion
    }
}
