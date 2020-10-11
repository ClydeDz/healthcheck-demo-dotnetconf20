using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Demo.API
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
            services.AddCors();
            services.AddHealthChecks()
                .AddSqlServer(Configuration["ConnectionStrings:dbConnectionString"], tags: new[] { "lite", "full" })
                .AddAzureBlobStorage(Configuration["ConnectionStrings:blobConnectionString"], tags: new[] { "full" })
                .AddUrlGroup(new Uri("https://adventureworks7943.azurewebsites.net/health"), tags: new[] { "full" }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("lite")
                });
                endpoints.MapHealthChecks("/health/full", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("full")
                });
            });
        }
    }
}
