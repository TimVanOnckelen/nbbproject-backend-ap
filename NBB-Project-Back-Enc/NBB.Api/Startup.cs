using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using NBB.Api.Data;
using NBB.Api.Models;
using NBB.Api.Repository;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using NBB.Api.Repositories;
using NBB.Api.Services;
using System.Text;

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

            services.AddTransient<IDbService<Enterprise>, DbService<Enterprise>>();
            services.AddTransient<IRepository, DbRepoEnterprises>();

            var connection = configuration.GetConnectionString("Server=localhost;Database=master;Trusted_Connection=True;");
            services.AddDbContext<NbbDbContext<Enterprise>>(options =>
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;Database=nbb;"));
            services.AddControllers();
            services.AddSwaggerGen();

            //services.AddDbContext<NbbDbContext<User>>(options =>
            // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IAuthenticationService, AuthenticationService>();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<NbbDbContext<User>>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var sKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:ServerSecret"]));
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = sKey,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Issuer"]
                    };
                });
            services.AddTransient<IRepository<Enterprise>, InMemoryDB<Enterprise>>();
            services.AddTransient<IRepository<User>, InMemoryDB<User>>();
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
               //app.UseCors(builder =>
                //{
                //    builder
                //    .AllowAnyOrigin()
                //    .AllowAnyMethod()
                //    .AllowAnyHeader();
                //});
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("OOPS")
                });
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

    }
}


