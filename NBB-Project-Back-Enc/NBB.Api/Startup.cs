using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using NBB.Api.Models;
using NBB.Api.Repository;
using NBB.Api.services;

namespace NBB.Api
{
    public class Startup
    {
        private IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRepository, MySQLEnterprise>();
            services.AddControllers();
            services.AddSwaggerGen();
            var connection = configuration.GetConnectionString("NBBDatabase");
            services.AddDbContext<EnterpriseDbContext>(x => x.UseMySql(connection, ServerVersion.AutoDetect(connection)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("./swagger/v1/swagger.json", "NBB API");
                    c.RoutePrefix = String.Empty;
                });

                // TO BE EDITED!
                app.UseCors(builder =>
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("OOPS")
                });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
