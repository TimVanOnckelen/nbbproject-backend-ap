using Microsoft.AspNetCore.Builder;

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

            //services.AddScoped<IContractRepository, EfContractRepository>();
            services.AddControllers();
            services.AddSwaggerGen();
            var connection = configuration.GetConnectionString("NBBDatabase");
            //services.AddDbContext<ContractDBContext>(x => x.UseMySql(connection, ServerVersion.AutoDetect(connection)));
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
