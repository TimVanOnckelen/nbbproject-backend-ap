using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NBB.Api.Entities;
using NBB.Api.Services;

namespace NBB.Api
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    var sKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:ServerSecret"]));
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = sKey,
                        ValidIssuer = _configuration["JWT:Issuer"],
                        ValidAudience = _configuration["JWT:Issuer"]
                    };
                });
            services.AddScoped<IEnterpriseRepository, EfNBBRepository>();
            services.AddControllers();
            services.AddSwaggerGen();
            var connection = _configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
            services.AddDbContext<NBBDBContext>(x => x.UseSqlServer(connection));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("OOPS")
                });
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "NBB API");
                c.RoutePrefix = String.Empty;
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }

}
