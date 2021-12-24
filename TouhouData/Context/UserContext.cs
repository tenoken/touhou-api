using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TouhouData.Context
{
    public class UserContext : IdentityDbContext<IdentityUser>
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUser>().HasKey(i => i.Id);
            base.OnModelCreating(builder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //var connectionString = @"Data Source=LAPTOP-AILPR68G;Initial Catalog=Touhou;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True";
        //    //var connectionString = @"Data Source=LAPTOP-AILPR68G;Initial Catalog=TouhouUsers;Integrated Security=True;Persist Security Info=True;User ID=sa;Password=@oisora17";
        //    //optionsBuilder.UseSqlServer(connectionString);
        //    //base.OnConfiguring(optionsBuilder);
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("UserConnection"), b => b.MigrationsAssembly("TouhouData"));
        }
    }
}
