using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ApiServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var identityUrl = Environment.GetEnvironmentVariable("IdentityUrl");
            if (string.IsNullOrEmpty(identityUrl))
            {
                identityUrl = "http://localhost:5000";
            }

            Console.WriteLine("================ API SERVER Start ================");
            Console.WriteLine($"identityUrl:{identityUrl}");

            services.AddControllers();

            services.AddAuthentication("Bearer")                
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = identityUrl;
                    options.RequireHttpsMetadata = false;                    
                    options.Audience = "api1";
                });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5003")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("default");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
