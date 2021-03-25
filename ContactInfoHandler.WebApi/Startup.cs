using ContactInfoHandler.Application.Core.Areas.Configuration;
using ContactInfoHandler.Application.Core.Identifications.Configuration;
using ContactInfoHandler.Application.Core.Persons.Configuration;
using ContactInfoHandler.Infrastructure.Data.Persistence.Core.Base.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ContactInfoHandler.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            var dbSettings = Configuration.GetSection("DbConnectionString").Get<string>();

            services.ConfigureKindOfIdentification(new DbSettings { ConnectionString = dbSettings });
            services.ConfigureAreaOfWork(new DbSettings { ConnectionString = dbSettings });
            services.ConfigurePersons(new DbSettings { ConnectionString = dbSettings });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
