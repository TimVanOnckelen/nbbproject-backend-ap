using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using NBB.Api.Data;
using NBB.Api.Models;
using NBB.Api.Repository;
using System.Configuration;
using Microsoft.EntityFrameworkCore;


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

            services.AddScoped<IRepository, InMemoryDB>();
            services.AddControllers();
            services.AddSwaggerGen();
            var connection = configuration.GetConnectionString("Server=localhost;Database=master;Trusted_Connection=True;");
            services.AddDbContext<NbbDbContext<Enterprise>>(options =>
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=nbb;"));
            //services.AddDbContext<NbbDbContext<User>>(options =>
            // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

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
