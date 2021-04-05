using ContactInfoHandler.Application.Core.Areas.Configuration;
using ContactInfoHandler.Application.Core.Areas.Service;
using ContactInfoHandler.Application.Core.Base.Exceptions;
using ContactInfoHandler.Application.Core.Identifications.Configuration;
using ContactInfoHandler.Application.Core.Identifications.Service;
using ContactInfoHandler.Application.Core.Persons.Configuration;
using ContactInfoHandler.Application.Core.Persons.Employees.Service;
using ContactInfoHandler.Application.Dto.Areas;
using ContactInfoHandler.Application.Dto.Identifications;
using ContactInfoHandler.Application.Dto.Persons.Employees;
using ContactInfoHandler.Dominio.Core.Identifications;
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
        #region UnitTests        

       #region Insert Tests

        [Fact]
        [UnitTest]
        public async Task InsertFailKindAndNumberIdExistingOrSameNameAndKindOfPerson()
        {
            var EmployeeMock = new Mock<IEmployeeRepository>();
            var employees = new List<EmployeeEntity>();
            employees.Add(new EmployeeEntity { FirstName = "Any Name" });
            IEnumerable<EmployeeEntity> employeesNum = employees;
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .Returns(() => Task.FromResult(employeesNum));

            var KindOfIdMock = new Mock<IKindOfIdentificationRepository>();
            KindOfIdMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
                .Returns(() => Task.FromResult(new KindOfIdentificationEntity
                {
                    IdentificationName = "Any",
                    KindOfIdentificationId = Guid.NewGuid()
                }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => EmployeeMock.Object);
            services.AddTransient(_ => KindOfIdMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var EmployeeService = provider.GetRequiredService<IEmployeeService>();

            await Assert.ThrowsAsync<PersonWithSameParametersExistingException>(() => EmployeeService.InsertEmployee(new EmployeeDto {
                KindOfPerson = "Natural"
            }));
        }

        [Fact]
        [UnitTest]
        public async Task InsertFailEmployeeJuridic()
        {
            var services = new ServiceCollection();           
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var EmployeeService = provider.GetRequiredService<IEmployeeService>();

            await Assert.ThrowsAsync<JuridicEmployeeException>(() => EmployeeService.InsertEmployee(new EmployeeDto
            {
                KindOfPerson = "Juridica"
            }));
        }

        [Fact]
        [UnitTest]
        public async Task InsertFailKindOfIdNull()
        {
            var services = new ServiceCollection();
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var EmployeeService = provider.GetRequiredService<IEmployeeService>();


            await Assert.ThrowsAsync<InvalidKindOfPerson>(() => EmployeeService.InsertEmployee(new EmployeeDto
            {
            }));
        }


        [Fact]
        [UnitTest]
        public async Task InsertFailsKindOfIdIsNit()
        {
            var EmployeeMock = new Mock<IEmployeeRepository>();
            IEnumerable<EmployeeEntity> employees = new List<EmployeeEntity>();
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .Returns(() => Task.FromResult(employees));

            var KindOfIdMock = new Mock<IKindOfIdentificationRepository>();
            KindOfIdMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
                .Returns(() => Task.FromResult(new KindOfIdentificationEntity
                {
                    IdentificationName = "Any",
                    KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
                }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => EmployeeMock.Object);
            services.AddTransient(_ => KindOfIdMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var employeeService = provider.GetRequiredService<IEmployeeService>();

            await Assert.ThrowsAsync<EmployeeWithNitException>(() => employeeService.InsertEmployee(new EmployeeDto
            {
                KindOfPerson = "Natural",
                DateOfBirth = DateTimeOffset.Now,
                SignUpDate = DateTimeOffset.Now,
                KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
            }
            ));

        }

        [Fact]
        [UnitTest]
        public async Task InsertFailSingUpDateNull()
        {
            var EmployeeMock = new Mock<IEmployeeRepository>();
            IEnumerable<EmployeeEntity> employes = new List<EmployeeEntity>();
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .Returns(() => Task.FromResult(employes));

            var KindOfIdMock = new Mock<IKindOfIdentificationRepository>();
            KindOfIdMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
                .Returns(() => Task.FromResult(new KindOfIdentificationEntity
                {
                    IdentificationName = "Any",
                    KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
                }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => EmployeeMock.Object);
            services.AddTransient(_ => KindOfIdMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var EmployeeService = provider.GetRequiredService<IEmployeeService>();

            await Assert.ThrowsAsync<SignUpDateMissingException>(() => EmployeeService.InsertEmployee(new EmployeeDto
            {
                DateOfBirth = DateTimeOffset.Now,
                KindOfPerson = "Natural"
            }));
        }

        [Fact]
        [UnitTest]
        public async Task InsertFailBirthDateNull()
        {
            var EmployeeMock = new Mock<IEmployeeRepository>();
            IEnumerable<EmployeeEntity> employes = new List<EmployeeEntity>();
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .Returns(() => Task.FromResult(employes));

            var KindOfIdMock = new Mock<IKindOfIdentificationRepository>();
            KindOfIdMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>()))
                .Returns(() => Task.FromResult(new KindOfIdentificationEntity
                {
                    IdentificationName = "Any",
                    KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
                }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => EmployeeMock.Object);
            services.AddTransient(_ => KindOfIdMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var EmployeeService = provider.GetRequiredService<IEmployeeService>();

            await Assert.ThrowsAsync<DateOfBirthMissingException>(() => EmployeeService.InsertEmployee(new EmployeeDto
            {
                SignUpDate = DateTimeOffset.Now,
                KindOfPerson = "Natural"
            }));
        }


        [Fact]
        [UnitTest]
        public async Task InsertSuccesfullPerson()
        {
            var EmployeeMock = new Mock<IEmployeeRepository>();
            IEnumerable<EmployeeEntity> employes = new List<EmployeeEntity>();
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .Returns(() => Task.FromResult(employes));

            var KindFoIdentificarionMock = new Mock<IKindOfIdentificationRepository>();
            KindFoIdentificarionMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>())).Returns(() => Task.FromResult(new KindOfIdentificationEntity
            {
                IdentificationName = "Any",
                KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
            }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => EmployeeMock.Object);
            services.AddTransient(_ => KindFoIdentificarionMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var employeeService = provider.GetRequiredService<IEmployeeService>();

            var response = await employeeService.InsertEmployee(new EmployeeDto
            {
                EmployeeCode= Guid.NewGuid(),
                AreaId = Guid.NewGuid(),
                IdEmployee = Guid.NewGuid(),
                IdNumber = 252819547,
                KindOfIdentificationId = new Guid("D008599C-0EF9-4E91-8E5D-CD349B391153"),
                DateOfBirth = DateTimeOffset.Now,
                SignUpDate = DateTimeOffset.Now,
                KindOfPerson = "Natural"
            }).ConfigureAwait(false);

            Assert.True(response);
        }


        [Fact]
        [UnitTest]
        public async Task UnitFailInsertNullArea()
        {
            var EmployeeMock = new Mock<IEmployeeRepository>();
            IEnumerable<EmployeeEntity> employes = new List<EmployeeEntity>();
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .Returns(() => Task.FromResult(employes));

            var KindFoIdentificarionMock = new Mock<IKindOfIdentificationRepository>();
            KindFoIdentificarionMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>())).Returns(() => Task.FromResult(new KindOfIdentificationEntity
            {
                IdentificationName = "Any",
                KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
            }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => EmployeeMock.Object);
            services.AddTransient(_ => KindFoIdentificarionMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var employeeService = provider.GetRequiredService<IEmployeeService>();

            await Assert.ThrowsAsync<NoAreaEspecifiedException>(() => employeeService.InsertEmployee(new EmployeeDto 
            {
                IdEmployee = Guid.NewGuid(),
                EmployeeCode = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Natural",
            }
            ));

        }

        [Fact]
        [UnitTest]
        public async Task FailInsertUnitNullEmployeeCode()
        {
            var EmployeeMock = new Mock<IEmployeeRepository>();
            IEnumerable<EmployeeEntity> employes = new List<EmployeeEntity>();
            EmployeeMock.Setup(x => x.SearchMatching(It.IsAny<Expression<Func<EmployeeEntity, bool>>>()))
            .Returns(() => Task.FromResult(employes));

            var KindFoIdentificarionMock = new Mock<IKindOfIdentificationRepository>();
            KindFoIdentificarionMock.Setup(x => x.GetOne(It.IsAny<Expression<Func<KindOfIdentificationEntity, bool>>>())).Returns(() => Task.FromResult(new KindOfIdentificationEntity
            {
                IdentificationName = "Any",
                KindOfIdentificationId = new Guid("DC29CCA5-E678-4F54-A179-5B935822E9F6")
            }
            ));

            var services = new ServiceCollection();
            services.AddTransient(_ => EmployeeMock.Object);
            services.AddTransient(_ => KindFoIdentificarionMock.Object);
            services.ConfigurePersons(new DbSettings());
            var provider = services.BuildServiceProvider();
            var employeeService = provider.GetRequiredService<IEmployeeService>();

            await Assert.ThrowsAsync<NoEspecifiedEmployeeCodeException>(() => employeeService.InsertEmployee(new EmployeeDto
            {
                IdEmployee = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Natural",
            }
            ));
        }

        #endregion

        #endregion

        long IdNumber = 1061822539;

        #region Integration Tests        

        #region InsertTests

      
        [Fact]
        [IntegrationTest]
        public async Task FailJuridicEmployee()
        {
            var service = new ServiceCollection();
            var KoiserviceCollector = new ServiceCollection();

            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            KoiserviceCollector.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();
            var koiProvider = KoiserviceCollector.BuildServiceProvider();

            var employeeService = provider.GetRequiredService<IEmployeeService>();
            var KoiService = koiProvider.GetRequiredService<IKindOfIdentificationService>();

            string koi = "Cedula";
            var KindOfIdCedulaKoiService = await KoiService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);

            await Assert.ThrowsAsync<JuridicEmployeeException>(() => employeeService.InsertEmployee(new EmployeeDto
            {
                AreaId = Guid.NewGuid(),
                IdEmployee = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = KindOfIdCedulaKoiService.KindOfIdentificationId,
                KindOfPerson = "Juridica",
            }));

        }

        [Fact]
        [IntegrationTest]
        public async Task FailInsertNullArea()
        {
            var service = new ServiceCollection();
            var KoiserviceCollector = new ServiceCollection();

            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            KoiserviceCollector.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();
            var koiProvider = KoiserviceCollector.BuildServiceProvider();

            var employeeService = provider.GetRequiredService<IEmployeeService>();
            var KoiService = koiProvider.GetRequiredService<IKindOfIdentificationService>();

            string koi = "Cedula";
            var KindOfIdCedulaKoiService = await KoiService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);

            await Assert.ThrowsAsync<NoAreaEspecifiedException>(() => employeeService.InsertEmployee(new EmployeeDto
            {
                IdEmployee = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = KindOfIdCedulaKoiService.KindOfIdentificationId,
                KindOfPerson = "Natural",
                EmployeeCode = Guid.NewGuid()
            }));

        }

        [Fact]
        [IntegrationTest]
        public async Task FailInsertNullEmployeeCode()
        {
            var service = new ServiceCollection();
            var KoiserviceCollector = new ServiceCollection();

            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            KoiserviceCollector.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var provider = service.BuildServiceProvider();
            var koiProvider = KoiserviceCollector.BuildServiceProvider();

            var employeeService = provider.GetRequiredService<IEmployeeService>();
            var KoiService = koiProvider.GetRequiredService<IKindOfIdentificationService>();

            string koi = "Cedula";
            var KindOfIdCedulaKoiService = await KoiService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);

            await Assert.ThrowsAsync<NoEspecifiedEmployeeCodeException>(() => employeeService.InsertEmployee(new EmployeeDto
            {
                IdEmployee = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = KindOfIdCedulaKoiService.KindOfIdentificationId,
                KindOfPerson = "Natural",
            }));
        }

        [Fact]
        [IntegrationTest]
        public async Task SuccesfullInsertEmployee()
        {
            var service = new ServiceCollection();
            var KoiserviceCollector = new ServiceCollection();
            var AreaServiceCollector = new ServiceCollection();

            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            KoiserviceCollector.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            AreaServiceCollector.ConfigureAreaOfWork(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });


            var provider = service.BuildServiceProvider();
            var koiProvider = KoiserviceCollector.BuildServiceProvider();
            var areaOfWorkProvider = AreaServiceCollector.BuildServiceProvider();

            var employeeService = provider.GetRequiredService<IEmployeeService>();
            var KoiService = koiProvider.GetRequiredService<IKindOfIdentificationService>();
            var AreaOfWorkService = areaOfWorkProvider.GetRequiredService<IAreaOfWorkService>();

            string koi = "Cedula";
            var KindOfIdCedulaKoiService = await KoiService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);

            var AddedArea = new AreaOfWorkDto { AreaName = "Talento Humano", AreaId = Guid.NewGuid(), ResponsableEmployeeId = Guid.NewGuid() };

            await AreaOfWorkService.InsertArea(AddedArea);

            string areaOfWork = "Talento Humano";
            var AreaOfWorkTH = await AreaOfWorkService.GetOne(new AreaOfWorkDto { AreaName = areaOfWork }).ConfigureAwait(false);

            var AddedEmployee = new EmployeeDto
            {
                EmployeeCode= Guid.NewGuid(),
                AreaId = AreaOfWorkTH.AreaId,
                IdEmployee = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = KindOfIdCedulaKoiService.KindOfIdentificationId,
                KindOfPerson = "Natural",
            };

            var response = await employeeService.InsertEmployee(AddedEmployee).ConfigureAwait(false);
            Assert.True(response);
            await employeeService.DeleteEmployee(AddedEmployee);
            await AreaOfWorkService.DeleteArea(AddedArea);

        }

        [Fact]
        [IntegrationTest]
        public async Task FailInsertExistingEmployeeCode()
        {

            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var KindofIdServiceCollector = new ServiceCollection();
            KindofIdServiceCollector.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });


            var Employeeprovider = service.BuildServiceProvider();
            var kindofidprovider = KindofIdServiceCollector.BuildServiceProvider();

            var EmployeeService = Employeeprovider.GetRequiredService<IEmployeeService>();
            var KindOfIdService = kindofidprovider.GetRequiredService<IKindOfIdentificationService>();

            string koi = "Cedula";
            var KindOfIdCedula = await KindOfIdService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);

            var employeeCode = Guid.NewGuid();

            var AddedEmployee = new EmployeeDto
            {
                AreaId = Guid.NewGuid(),
                IdEmployee = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = KindOfIdCedula.KindOfIdentificationId,
                KindOfPerson = "Natural",
                EmployeeCode = employeeCode
            };

            var response = await EmployeeService.InsertEmployee(AddedEmployee).ConfigureAwait(false);

            await Assert.ThrowsAsync<AlreadyExistingEmployeeCodeException>(() => EmployeeService.InsertEmployee(new EmployeeDto
            {
                AreaId = Guid.NewGuid(),
                IdEmployee = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = 25280922,
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Natural",
                EmployeeCode = employeeCode
            }));

            await EmployeeService.DeleteEmployee(AddedEmployee).ConfigureAwait(false);

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

            var KindofIdServiceCollector = new ServiceCollection();
            KindofIdServiceCollector.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });


            var Employeeprovider = service.BuildServiceProvider();
            var kindofidprovider = KindofIdServiceCollector.BuildServiceProvider();

            var EmployeeService = Employeeprovider.GetRequiredService<IEmployeeService>();
            var KindOfIdService = kindofidprovider.GetRequiredService<IKindOfIdentificationService>();

            string koi = "Cedula";
            var KindOfIdCedula = await KindOfIdService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);

            var AddedEmployee = new EmployeeDto
            {
                AreaId = Guid.NewGuid(),
                IdEmployee = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = KindOfIdCedula.KindOfIdentificationId,
                KindOfPerson = "Natural",
                EmployeeCode = Guid.NewGuid(),
            };

            var response = await EmployeeService.InsertEmployee(AddedEmployee).ConfigureAwait(false);

            await Assert.ThrowsAsync<PersonWithSameParametersExistingException>(() => EmployeeService.InsertEmployee(new EmployeeDto
            {
                AreaId = Guid.NewGuid(),
                IdEmployee = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = KindOfIdCedula.KindOfIdentificationId,
                KindOfPerson = "Natural",
                EmployeeCode = Guid.NewGuid(),
            }));

            await EmployeeService.DeleteEmployee(AddedEmployee).ConfigureAwait(false);

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

            var KindofIdServiceCollector = new ServiceCollection();
            KindofIdServiceCollector.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });


            var Employeeprovider = service.BuildServiceProvider();
            var kindofidprovider = KindofIdServiceCollector.BuildServiceProvider();

            var EmployeeService = Employeeprovider.GetRequiredService<IEmployeeService>();
            var KindOfIdService = kindofidprovider.GetRequiredService<IKindOfIdentificationService>();

           
            

            string koi = "Cedula";
            var KindOfIdCedula = await KindOfIdService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);

            var AddedEmployee = new EmployeeDto
            {
                AreaId = Guid.NewGuid(),
                IdEmployee = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = KindOfIdCedula.KindOfIdentificationId,
                KindOfPerson = "Natural",
                EmployeeCode = Guid.NewGuid()
            };

            string koiP = "Pasaporte";
            var KindOfIdPasaporte = await KindOfIdService.GetOne(new KindOfIdentificationDto { IdentificationName = koiP }).ConfigureAwait(false);

            var response = await EmployeeService.InsertEmployee(AddedEmployee).ConfigureAwait(false);

            await Assert.ThrowsAsync<PersonWithSameParametersExistingException>(() => EmployeeService.InsertEmployee(new EmployeeDto
            {
                AreaId = Guid.NewGuid(),
                IdEmployee = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = KindOfIdPasaporte.KindOfIdentificationId,
                KindOfPerson = "Natural",
                EmployeeCode = Guid.NewGuid(),
            }));

            await EmployeeService.DeleteEmployee(AddedEmployee).ConfigureAwait(false);
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

            var employeeService = provider.GetRequiredService<IEmployeeService>();
            var KindOfIdentificationService = KindOfIdentificationProvider.GetRequiredService<IKindOfIdentificationService>();

            var verifyNitOnDb = KindOfIdentificationService.VerifyExisting(new KindOfIdentificationDto { IdentificationName = "Nit" });

            string koi = "Nit";

            if (verifyNitOnDb.Result)
            {
                var KindOfIdNitA = await KindOfIdentificationService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);

                await Assert.ThrowsAsync<EmployeeWithNitException>(() => employeeService.InsertEmployee(new EmployeeDto
                {
                    AreaId = Guid.NewGuid(),
                    IdEmployee = Guid.NewGuid(),
                    EmployeeCode = Guid.NewGuid(),
                    DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                    FirstName = "Andres",
                    SecondName = "Manuel",
                    FirstLastName = "Castro",
                    SecondLastName = "Lopez",
                    SignUpDate = DateTimeOffset.Now,
                    IdNumber = 1061827731,
                    KindOfPerson = "Natural",
                    KindOfIdentificationId = KindOfIdNitA.KindOfIdentificationId
                }));
            }
            else 
            {
            await KindOfIdentificationService.InsertKindOfId(new KindOfIdentificationDto { IdentificationName = "Nit", KindOfIdentificationId = Guid.NewGuid() });
            var KindOfIdNitB = await KindOfIdentificationService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);
            await Assert.ThrowsAsync<EmployeeWithNitException>(() => employeeService.InsertEmployee(new EmployeeDto
            {
                AreaId = Guid.NewGuid(),
                IdEmployee = Guid.NewGuid(),
                EmployeeCode = Guid.NewGuid(),
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = 1061827731,
                KindOfPerson = "Natural",
                KindOfIdentificationId = KindOfIdNitB.KindOfIdentificationId
            }));
            }
        }

        [Fact]
        [IntegrationTest]
        public async Task FailNullBirthDate()
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

            var employeeService = provider.GetRequiredService<IEmployeeService>();
            var KindOfIdentificationService = KindOfIdentificationProvider.GetRequiredService<IKindOfIdentificationService>();


            string koi = "Nit";
            var KindOfIdNit = await KindOfIdentificationService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);


            await Assert.ThrowsAsync<DateOfBirthMissingException>(() => employeeService.InsertEmployee(new EmployeeDto
            {
                SignUpDate = DateTimeOffset.Now,
                AreaId = Guid.NewGuid(),
                IdEmployee = Guid.NewGuid(),
                EmployeeCode = Guid.NewGuid(),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                IdNumber = 1061827731,
                KindOfPerson = "Natural",
                KindOfIdentificationId = KindOfIdNit.KindOfIdentificationId
            }));
        }

        [Fact]
        [IntegrationTest]
        public async Task FailNullSignUpDate()
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

            var employeeService = provider.GetRequiredService<IEmployeeService>();
            var KindOfIdentificationService = KindOfIdentificationProvider.GetRequiredService<IKindOfIdentificationService>();


            string koi = "Nit";
            var KindOfIdNit = await KindOfIdentificationService.GetOne(new KindOfIdentificationDto { IdentificationName = koi }).ConfigureAwait(false);


            await Assert.ThrowsAsync<SignUpDateMissingException>(() => employeeService.InsertEmployee(new EmployeeDto
            {
                DateOfBirth = DateTimeOffset.Now,
                AreaId = Guid.NewGuid(),
                IdEmployee = Guid.NewGuid(),
                EmployeeCode = Guid.NewGuid(),
                FirstName = "Andres",
                SecondName = "Manuel",
                FirstLastName = "Castro",
                SecondLastName = "Lopez",
                IdNumber = 1061827731,
                KindOfPerson = "Natural",
                KindOfIdentificationId = KindOfIdNit.KindOfIdentificationId
            }));
        }

        #endregion

        #region DeleteTests

        [Fact]
        [IntegrationTest]
        public async Task SuccesfullDeleteEmployee()
        {
            var service = new ServiceCollection();
            var kindOfIdService = new ServiceCollection();

            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            kindOfIdService.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });


            var provider = service.BuildServiceProvider();
            var providerkindOfId = kindOfIdService.BuildServiceProvider();

            var employeeService = provider.GetRequiredService<IEmployeeService>();
            var KindOfIdService = providerkindOfId.GetRequiredService<IKindOfIdentificationService>();

            var kindOfId = "Cedula";
            var KindOfIdEntity = await KindOfIdService.GetOne(new KindOfIdentificationDto { IdentificationName = kindOfId });

            var AddedEmployee = new EmployeeDto
            {
                EmployeeCode = Guid.NewGuid(),
                KindOfPerson = "Natural",
                AreaId = Guid.NewGuid(),
                KindOfIdentificationId = KindOfIdEntity.KindOfIdentificationId,
                DateOfBirth = DateTimeOffset.Now,
                SignUpDate = DateTimeOffset.Now,
                IdNumber = 5418498,
                IdEmployee = Guid.NewGuid()
            };

            await employeeService.InsertEmployee(AddedEmployee);

            var response = await employeeService.DeleteEmployee(AddedEmployee).ConfigureAwait(false);

            Assert.True(response);
        }

        [Fact]
        [IntegrationTest]

        public async Task FailDeleteNoExistingEmployee()
        {
            var EmployeeServiceCollector = new ServiceCollection();

            EmployeeServiceCollector.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var EmployeeProvider = EmployeeServiceCollector.BuildServiceProvider();

            var EmployeeService = EmployeeProvider.GetRequiredService<IEmployeeService>();

            await Assert.ThrowsAsync<NoExistingEmployee>(() => EmployeeService.DeleteEmployee(new EmployeeDto
            {
                IdEmployee = Guid.NewGuid()
            }));

        }

        #endregion

        #region UpdateTests

        [Fact]
        [IntegrationTest]

        public async Task SuccesfullEmployeeUpdate() 
        {
            var service = new ServiceCollection();
            service.ConfigurePersons(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });

            var serviceKindOfId = new ServiceCollection();
            serviceKindOfId.ConfigureKindOfIdentification(new DbSettings
            {
                ConnectionString = "Server = DESKTOP-QHO5U57\\MYSQLFORBLAZOR; Database=PersonsContactInfo;Trusted_Connection=True;"
            });


            var provider = service.BuildServiceProvider();
            var KoiProvider = serviceKindOfId.BuildServiceProvider();

            var EmployeeService = provider.GetRequiredService<IEmployeeService>();
            var KindOfIdService = KoiProvider.GetRequiredService<IKindOfIdentificationService>();

            var KindOfId = "Cedula";
            var Cedula = await KindOfIdService.GetOne(new KindOfIdentificationDto { IdentificationName = KindOfId });

            var IdEmployeeToEdit = Guid.NewGuid();

            var AddedEmployee = new EmployeeDto
            {
                IdEmployee = IdEmployeeToEdit,                
                AreaId = Guid.NewGuid(),
                EmployeeCode = Guid.NewGuid(),
                Salary = 0,
                DateOfBirth = new DateTimeOffset(new DateTime(1867, 1, 1), TimeSpan.Zero),
                FirstName = "Sandra",
                SecondName = "Alkista",
                FirstLastName = "Lanzini",
                SecondLastName = "Lopez",
                SignUpDate = DateTimeOffset.Now,
                IdNumber = IdNumber,
                KindOfIdentificationId = Cedula.KindOfIdentificationId,
                KindOfPerson = "Natural"
            };

            await EmployeeService.InsertEmployee(AddedEmployee);

            var response = await EmployeeService.UpdateEmployee (new EmployeeDto
            {
                IdEmployee = IdEmployeeToEdit,
                Salary = 2000,
                SignUpDate = DateTimeOffset.Now,
                DateOfBirth = DateTimeOffset.Now,
                KindOfPerson = "Natural"
            });

            Assert.True(response);

            await EmployeeService.DeleteEmployee(AddedEmployee);
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

            var EmployeeService = provider.GetRequiredService<IEmployeeService>();

            await Assert.ThrowsAsync<NoExistingEmployee>(() => EmployeeService.UpdateEmployee(new EmployeeDto
            {
                IdEmployee = Guid.NewGuid(),
                DateOfBirth = DateTimeOffset.Now,
                SignUpDate = DateTimeOffset.Now
            }));


        }

        #endregion

        #endregion

    }

}