using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TouhouArticleMaker.Domain;

namespace TouhouData.EntityConfig
{
    class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(a => a.Title, title =>
            {
                title.Property(t => t.Text)
                  .HasColumnName("Title")
                  .IsRequired().HasMaxLength(50).HasColumnType("nchar(50)");
            });

            builder.OwnsOne(a => a.Developer, title =>
            {
                title.Property(t => t.Text)
                  .HasColumnName("Developer")
                  .IsRequired().HasMaxLength(50).HasColumnType("nchar(50)");
            });

            builder.OwnsOne(a => a.Publisher, title =>
            {
                title.Property(t => t.Text)
                  .HasColumnName("Publisher")
                  .IsRequired().HasMaxLength(50).HasColumnType("nchar(50)");
            });

            builder.OwnsOne(a => a.Genre, title =>
            {
                title.Property(t => t.Text)
                  .HasColumnName("Genre")
                  .IsRequired().HasMaxLength(50).HasColumnType("nchar(50)");
            });

            builder.OwnsOne(a => a.Requirements, title =>
            {
                title.Property(t => t.Text)
                  .HasColumnName("Requirements")
                  .IsRequired().HasMaxLength(50).HasColumnType("nchar(255)");
            });

            builder.OwnsOne(a => a.Platforms, title =>
            {
                title.Property(t => t.Text)
                  .HasColumnName("Platforms")
                  .IsRequired().HasMaxLength(50).HasColumnType("nchar(255)");
            });

            builder.OwnsOne(a => a.Gameplay, title =>
            {
                title.Property(t => t.Text)
                  .HasColumnName("Gameplay")
                  .IsRequired().HasMaxLength(50).HasColumnType("nchar(50)");
            });
        }
    }
}
