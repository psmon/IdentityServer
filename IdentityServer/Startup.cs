using System;
using System.Collections.Generic;
using IdentityServer.Config;
using IdentityServer.Entity;
using IdentityServer.Repositories;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // uncomment, if you want to add an MVC-based UI
            //services.AddControllersWithViews();

            var RedisUrl = Environment.GetEnvironmentVariable("RedisUrl");

            services.AddControllers();
            
            var builder = services.AddIdentityServer()
                .AddInMemoryIdentityResources(ServerConfig.Ids)
                .AddInMemoryApiResources(ServerConfig.Apis)
                .AddInMemoryClients(ServerConfig.Clients)
                .AddTestUsers(TestUserEntity.GetUsers());


            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();

            //Redis ¼³Á¤
            services.AddSingleton<UserRedisRepository>();
            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = RedisUrl;
                option.InstanceName = "identyuser:";
            });

        }

        public void Configure(IApplicationBuilder app)
        {
            //TODO : only dev
            app.UseDeveloperExceptionPage();

            // uncomment if you want to add MVC
            //app.UseStaticFiles();
            //app.UseRouting();

            app.UseIdentityServer();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // uncomment, if you want to add MVC
            //app.UseAuthorization();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapDefaultControllerRoute();
            //});
        }
    }
}
