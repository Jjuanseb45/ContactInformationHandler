using ContactInfoHandler.Application.Core.Areas.Service;
using ContactInfoHandler.Application.Core.Base.Exceptions;
using ContactInfoHandler.Application.Core.Identifications.Configuration;
using ContactInfoHandler.Application.Core.Identifications.Service;
using ContactInfoHandler.Application.Core.Persons.Configuration;
using ContactInfoHandler.Application.Core.Persons.Customers.Service;
using ContactInfoHandler.Application.Dto.Identifications;
using ContactInfoHandler.Application.Dto.Persons.Customers;
using ContactInfoHandler.Dominio.Core.Identifications;
using ContactInfoHandler.Dominio.Core.Persons.Customers;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace Test.ContactInfoHandler.Core._3._Application.Core.Persons.Customers
{
    public class CustomerSeriveTests
    {
        #region Tests Unitarios

        #region Test Insert

        [Fact]
        [UnitTest]
        public async Task InsertFailKindAndNumberIdExistingOrSameNameAndKindOfPerson()
        {
            var CustomerMock = new Mock<ICustomersRepository>();
            var customers = new List<CustomerEntity>();
            customers.Add(new CustomerEntity { FirstName = "Any Name" });
            IEnumerable<CustomerEntity> customersEnum = customers;
            CustomerMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
            .Returns(() => Task.FromResult(customersEnum));

            var KindOfIdMock = new Mock<IKindOfIdentificationRepository>();
            KindOfIdMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
                .Returns(() => Task.FromResult(new KindOfIdentificationEntity 
            {
                IdentificationName = "Any",
                KindOfIdentificationId = Guid.NewGuid()
            }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => CustomerMock.Object);
            services.AddTransient(_ => KindOfIdMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var CustomerService = provider.GetRequiredService<ICustomerService>();

            await Assert.ThrowsAsync<PersonWithSameParametersExistingException>(() => CustomerService.InsertCustomer(new CustomerDto { KindOfPerson="Juridica" }));
        }

        [Fact]
        [UnitTest]
        public async Task InsertFailNoKindOfPersonEspecified()
        {            

            var services = new ServiceCollection();

            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var CustomerService = provider.GetRequiredService<ICustomerService>();

            await Assert.ThrowsAsync<InvalidKindOfPerson>(() => CustomerService.InsertCustomer(new CustomerDto {  }));
        }

        [Fact]
        [UnitTest]
        public async Task InsertFailSingUpDateNull() 
        {
            var CustomerMock = new Mock<ICustomersRepository>();
            IEnumerable<CustomerEntity> customers = new List<CustomerEntity>();
            CustomerMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
            .Returns(() => Task.FromResult(customers));

            var KindOfIdMock = new Mock<IKindOfIdentificationRepository>();
            KindOfIdMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
                .Returns(() => Task.FromResult(new KindOfIdentificationEntity
                {
                    IdentificationName = "Any",
                    KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
                }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => CustomerMock.Object);
            services.AddTransient(_ => KindOfIdMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var CustomerService = provider.GetRequiredService<ICustomerService>();

            await Assert.ThrowsAsync<SignUpDateMissingException>(() => CustomerService.InsertCustomer(new CustomerDto 
            {
                IdCustmer = Guid.NewGuid(),
                KindOfPerson = "Natural",
                DateOfBirth = DateTimeOffset.Now
            }
            ));

        }

        [Fact]
        [UnitTest]
        public async Task InsertFailBirthDateNull()
        {
            var CustomerMock = new Mock<ICustomersRepository>();
            IEnumerable<CustomerEntity> customers = new List<CustomerEntity>();
            CustomerMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
            .Returns(() => Task.FromResult(customers));

            var KindOfIdMock = new Mock<IKindOfIdentificationRepository>();
            KindOfIdMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
                .Returns(() => Task.FromResult(new KindOfIdentificationEntity
                {
                    IdentificationName = "Any",
                    KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
                }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => CustomerMock.Object);
            services.AddTransient(_ => KindOfIdMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var CustomerService = provider.GetRequiredService<ICustomerService>();

            await Assert.ThrowsAsync<DateOfBirthMissingException>(() => CustomerService.InsertCustomer(new CustomerDto
            {
                IdCustmer = Guid.NewGuid(),
                KindOfPerson = "Natural",
                SignUpDate = DateTimeOffset.Now
            }
            ));

        }

        [Fact]
        [UnitTest]
        public async Task InsertFailsKindOfIdIsNit()
        {
            var CustomerMock = new Mock<ICustomersRepository>();
            IEnumerable<CustomerEntity> customers = new List<CustomerEntity>();
            CustomerMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
            .Returns(() => Task.FromResult(customers));

            var KindOfIdMock = new Mock<IKindOfIdentificationRepository>();
            KindOfIdMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
                .Returns(() => Task.FromResult(new KindOfIdentificationEntity
                {
                    IdentificationName = "Any",
                    KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
                }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => CustomerMock.Object);
            services.AddTransient(_ => KindOfIdMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var CustomerService = provider.GetRequiredService<ICustomerService>();

            await Assert.ThrowsAsync<EmployeeWithNitException>(() => CustomerService.InsertCustomer(new CustomerDto {            
                KindOfIdentificationId= new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6"),
                DateOfBirth=DateTimeOffset.Now,
                SignUpDate = DateTimeOffset.Now,
                KindOfPerson = "Juridica"
            }));
        }

        [Fact]
        [UnitTest]
        public async Task InsertSeccesfullPerson()
        {
            var CustomerMock = new Mock<ICustomersRepository>();
            IEnumerable<CustomerEntity> customers = new List<CustomerEntity>();
            CustomerMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
            .Returns(() => Task.FromResult(customers));

            var KindFoIdentificarionMock = new Mock<IKindOfIdentificationRepository>();
            KindFoIdentificarionMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>())).Returns(()=> Task.FromResult(new KindOfIdentificationEntity
            {
                IdentificationName = "Any",
                KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
            }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => CustomerMock.Object);
            services.AddTransient(_ => KindFoIdentificarionMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var CustomerService = provider.GetRequiredService<ICustomerService>();

            var response = await CustomerService.InsertCustomer(new CustomerDto
            {
                IdCustmer = Guid.NewGuid(),
                IdNumber = 252819547,
                KindOfIdentificationId = new Guid("D008599C-0EF9-4E91-8E5D-CD349B391153"),
                DateOfBirth = DateTimeOffset.Now,
                SignUpDate = DateTimeOffset.Now,
                KindOfPerson = "Natural"
            }).ConfigureAwait(false);
            Assert.True(response);
        }

        #endregion

        #endregion

        long IdNumber = 1061822639;
        Guid CustomerId = new Guid("ed33faa2-b547-423d-b398-cd750ad465ae");

        #region Tests Integration

        #region Test Update

        [Fact]
        [IntegrationTest]
        public async Task UpdateSuccesfull()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();

            var CustomerService = provider.GetRequiredService<ICustomerService>();

            var AddedCustomer = new CustomerDto
            {
                FavoriteBrand = "Gucci",
                IdCustmer = CustomerId,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Juridica",
            };

            await CustomerService.InsertCustomer(AddedCustomer);

            var response = await CustomerService.UpdateCustomer(new CustomerDto
            {
                IdCustmer = CustomerId,
                FavoriteBrand="Prada",
                KindOfIdentificationId = Guid.NewGuid()
            });
            Assert.True(response);

            await CustomerService.DeleteCustomer(AddedCustomer);
        }

        [Fact]
        [IntegrationTest]
        public async Task UpdateFailSuccesfullDoesntExist()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();

            var CustomerService = provider.GetRequiredService<ICustomerService>();

            await Assert.ThrowsAsync<NoExistingCustomerException>(() => CustomerService.UpdateCustomer(new CustomerDto
            {
                IdCustmer = Guid.NewGuid(),
                FavoriteBrand = "Prada",
            }));
        }

        #endregion

        #region Test Insert        

        [Fact]
        [IntegrationTest]
        public async Task MissingKindOfPerson() 
        {
            var service = new ServiceCollection();

            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();

            var CustomerService = provider.GetRequiredService<ICustomerService>();

            var AddedCustomer = new CustomerDto
            {
                FavoriteBrand = "Gucci",
                IdCustmer = CustomerId,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
            };
            await Assert.ThrowsAsync<InvalidKindOfPerson>(() => CustomerService.InsertCustomer(AddedCustomer));
        }

        [Fact]
        [IntegrationTest]
        public async Task NoValidKindOfPerson()
        {
            var service = new ServiceCollection();

            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();

            var CustomerService = provider.GetRequiredService<ICustomerService>();
          
            var AddedCustomer = new CustomerDto
            {
                KindOfPerson = "AnyKind",
                FavoriteBrand = "Gucci",
                IdCustmer = CustomerId,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
            };
            await Assert.ThrowsAsync<InvalidKindOfPerson>(() => CustomerService.InsertCustomer(AddedCustomer));
        }

        [Fact]
        [IntegrationTest]
        public async Task ASucessfullInsertCustomer()
        {
            var service = new ServiceCollection();

            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();

            var CustomerService = provider.GetRequiredService<ICustomerService>();       

            var AddedCustomer = new CustomerDto
            {
                FavoriteBrand = "Gucci",
                IdCustmer = CustomerId,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Juridica",                
            };

            var response = await CustomerService.InsertCustomer(AddedCustomer).ConfigureAwait(false);
            Assert.True(response);
            await CustomerService.DeleteCustomer(AddedCustomer);
        }

        [Fact]
        [IntegrationTest]
        public async Task BFailInsertExistingIdAndKindOfId()
        {

            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });         

            var provider = service.BuildServiceProvider();

            var CustomerService = provider.GetRequiredService<ICustomerService>();

            var KindOfIdentificationId = Guid.NewGuid();            

            var AddedCustomer = new CustomerDto
            {
                FavoriteBrand = "Gucci",
                IdCustmer = CustomerId,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = KindOfIdentificationId,
                KindOfPerson = "Juridica",
            };

            var response = await CustomerService.InsertCustomer(AddedCustomer).ConfigureAwait(false);

            await Assert.ThrowsAsync<PersonWithSameParametersExistingException>(() => CustomerService.InsertCustomer(new CustomerDto
            {
                FavoriteBrand = "Prada",
                IdCustmer = new Guid("75C1BE1C-AFB0-46AB-8C7B-86AC7C364C96"),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = KindOfIdentificationId,
                KindOfPerson = "Juridica"
            }));

            await CustomerService.DeleteCustomer(AddedCustomer).ConfigureAwait(false);

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

            var CustomerService = Employeeprovider.GetRequiredService<ICustomerService>();
           

            var AddedCustomer = new CustomerDto
            {
                FavoriteBrand="Gucci",
                IdCustmer = Guid.NewGuid(),
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
         
            var response = await CustomerService.InsertCustomer(AddedCustomer).ConfigureAwait(false);

            await Assert.ThrowsAsync<PersonWithSameParametersExistingException>(() => CustomerService.InsertCustomer(new CustomerDto
            {
                FavoriteBrand = "Trasher",
                IdCustmer = Guid.NewGuid(),
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

            await CustomerService.DeleteCustomer(AddedCustomer).ConfigureAwait(false);

        }

        [Fact]
        [IntegrationTest]
        public async Task CFailInsertNITDocument()
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
            var KindOfIdentificationProvider = KindOfIdServiceCollector.BuildServiceProvider();

            var CustomerService = provider.GetRequiredService<ICustomerService>();
            var KindOfIdentificationService = KindOfIdentificationProvider.GetRequiredService<IKindOfIdentificationService>();

            var verifyNitOnDb = KindOfIdentificationService.VerifyExisting(new KindOfIdentificationDto { IdentificationName = "Nit" });

            string koi = "Nit";

            if (verifyNitOnDb.Result)
            {
                var KindOfIdPasaporteKoiServiceA = await KindOfIdentificationService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);

                await Assert.ThrowsAsync<EmployeeWithNitException>(() => CustomerService.InsertCustomer(new CustomerDto
                {
                    FavoriteBrand = "Prada",
                    IdCustmer = new Guid("75C1BE1C-AFB0-46AB-8C7B-86AC7C364C91"),
                    DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                    FirstName = "Andres",
                    SecondName = "Manuel",
                    FirstLastName = "Castro",
                    SecondLastName = "Lopez",
                    SignUpDate = DateTimeOffset.Now,
                    IdNumber = 1061827731,
                    KindOfPerson = "Juridica",
                    KindOfIdentificationId = KindOfIdPasaporteKoiServiceA.KindOfIdentificationId
                }));
            }
            else
            { 
            await KindOfIdentificationService.InsertKindOfId(new KindOfIdentificationDto { IdentificationName = "Nit", KindOfIdentificationId = Guid.NewGuid()});
            var KindOfIdPasaporteKoiServiceB = await KindOfIdentificationService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);
            await Assert.ThrowsAsync<EmployeeWithNitException>(() => CustomerService.InsertCustomer(new CustomerDto
            {
                FavoriteBrand = "Prada",
                IdCustmer = new Guid("75C1BE1C-AFB0-46AB-8C7B-86AC7C364C91"),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = 1061827731,
                KindOfPerson = "Juridica",
                KindOfIdentificationId = KindOfIdPasaporteKoiServiceB.KindOfIdentificationId
            }));
            }
        }

        [Fact]
        [IntegrationTest]
        public async Task DFailBirthDateNull()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var CustomerService = provider.GetRequiredService<ICustomerService>();
            await Assert.ThrowsAsync<DateOfBirthMissingException>(() => CustomerService.InsertCustomer(new CustomerDto
            {   
                SignUpDate = DateTimeOffset.Now,
                FavoriteBrand = "Prada",
                IdCustmer = new Guid("75C1BE1C-AFB0-46AB-8C7B-86AC7C364C91"),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                IdNumber = IdNumber,
                KindOfPerson = "Juridica",
            }));
        }


        [Fact]
        [IntegrationTest]
        public async Task DFailSignUpDateNull()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
            var provider = service.BuildServiceProvider();
            var CustomerService = provider.GetRequiredService<ICustomerService>();
            await Assert.ThrowsAsync<SignUpDateMissingException>(() => CustomerService.InsertCustomer(new CustomerDto
            {
                DateOfBirth= DateTimeOffset.Now,
                FavoriteBrand = "Prada",
                IdCustmer = new Guid("75C1BE1C-AFB0-46AB-8C7B-86AC7C364C91"),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                IdNumber = IdNumber,
                KindOfPerson = "Juridica",
            }));
        }


        #endregion

        #region Tests Delete        
        //Test Delete

        [Fact]
        [IntegrationTest]
        public async Task ESuccesfullDeleteEmployee()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });
           
            var provider = service.BuildServiceProvider();

            var customerService = provider.GetRequiredService<ICustomerService>();
            
           await customerService.InsertCustomer(new CustomerDto
            {
                FavoriteBrand = "Prada",
                IdCustmer = CustomerId,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfPerson = "Juridica",
                KindOfIdentificationId = Guid.NewGuid()
            });

            var response = await customerService.DeleteCustomer(new CustomerDto
            {
                IdCustmer = CustomerId
            }).ConfigureAwait(false);

            Assert.True(response);
        }

        [Fact]
        [IntegrationTest]
        public async Task FailDeleteEmployeeNoExisting()
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();

            var customerService = provider.GetRequiredService<ICustomerService>();

            await Assert.ThrowsAsync<NoExistingCustomerException>(() => customerService.DeleteCustomer(new CustomerDto
            {
                IdCustmer = CustomerId
            }));

        }
        #endregion


        #endregion

    }
}
