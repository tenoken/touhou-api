using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using TouhouArticleMaker.Domain;
using TouhouData.EntityConfig;

namespace TouhouData.Context
{
    public class TouhouContext : DbContext
    {
        public TouhouContext(DbContextOptions<TouhouContext> options) : base(options) { }
 
        public DbSet<Article> Articles { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Gallery> Galleries { get; set; }

        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new ArticleConfiguration());
            modelBuilder.ApplyConfiguration(new SectionConfiguration());
            modelBuilder.ApplyConfiguration(new CardConfiguration());
            modelBuilder.ApplyConfiguration(new PhotoConfiguration());
            modelBuilder.ApplyConfiguration(new GalleryConfiguration());
            modelBuilder.Ignore<Notification>();

            //var keyProperties = modelBuilder
            //    .Model
            //    .GetEntityTypes()
            //    .SelectMany(e => e.GetProperties())
            //    .Where(p => p.Name == p.DeclaringEntityType.ClrType.Name + "_ID")
            //    .ToList();

            //foreach (var p in keyProperties)
            //{
            //    modelBuilder
            //        .Entity(p.DeclaringEntityType.Name)
            //        .HasKey(p.Name);
            //}

            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //var connectionString = @"Data Source=LAPTOP-AILPR68G;Initial Catalog=Touhou;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=True";
        //    //var connectionString = @"Data Source=LAPTOP-AILPR68G;Initial Catalog=Touhou;Integrated Security=True;Persist Security Info=True;User ID=sa;Password=@oisora17";
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

            optionsBuilder.UseSqlServer(config.GetConnectionString("TouhouConnection"), b => b.MigrationsAssembly("TouhouData"));
        }

    }
}
