using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Config;
using IdentityServer.Models.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Repositories
{    
    public class UserRepository : IdentityDbContext<ApplicationUser>
    {
        private string database = "authdb";

        private readonly AppSettings _appSettings;

        public UserRepository(AppSettings appSettings)            
        {
            _appSettings = appSettings;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string dbOption = "";
                string dbConnectionString = _appSettings.DBConnectionAuth + $"database={database};" + dbOption;
                optionsBuilder.UseMySql(dbConnectionString);
            }
        }
    }
}
