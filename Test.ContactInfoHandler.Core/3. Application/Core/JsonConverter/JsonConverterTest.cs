using ContactInfoHandler.Application.Core.JsonConverter.Base.Exceptions;
using ContactInfoHandler.Application.Core.JsonConverter.Configuration;
using ContactInfoHandler.Application.Core.JsonConverter.Service;
using ContactInfoHandler.Application.Dto.Persons.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Categories;

namespace Test.ContactInfoHandler.Core._3._Application.Core.JsonConverter
{
    public class JsonConverterTest
    {

        [Fact]
        [UnitTest]
        public async Task FailConversionNullPath() 
        {
            var service = new ServiceCollection();
            service.ConfigureConverter();
            var provider = service.BuildServiceProvider();
            var CoverterService = provider.GetRequiredService<IConverterService>();

            var ListProvider = new List<ProviderDto>();
            ListProvider.Add( new ProviderDto
            {
                IdNumber = 487498,
                CompanyName = "AnyCompany",
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Natural",
                SignUpDate = DateTimeOffset.Now,
                DateOfBirth = DateTimeOffset.Now
            });
            await Assert.ThrowsAsync<NullPathToConvertException>(async () => await CoverterService.ExportJson("", ListProvider).ConfigureAwait(false));
        }

        [Fact]
        [IntegrationTest]
        public async Task SuccesfullExport() 
        {
            var service = new ServiceCollection();
            service.ConfigureConverter();
            var provider = service.BuildServiceProvider();
            var CoverterService = provider.GetRequiredService<IConverterService>();

            var ListProvider = new List<ProviderDto>();
            ListProvider.Add(new ProviderDto
            {
                IdNumber = 487498,
                CompanyName = "AnyCompany",
                KindOfIdentificationId = Guid.NewGuid(),
                KindOfPerson = "Natural",
                SignUpDate = DateTimeOffset.Now,
                DateOfBirth = DateTimeOffset.Now
            });
            var response = await CoverterService.ExportJson("C:\\Users\\Usuario\\Desktop\\PruebaJson", ListProvider).ConfigureAwait(false);
            Assert.NotNull(response);
        } 
    }
}
