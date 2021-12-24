using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TouhouArticleMaker.Domain;

namespace TouhouData.EntityConfig
{
    class PhotoConfiguration : IEntityTypeConfiguration<Photo>
    {
        public void Configure(EntityTypeBuilder<Photo> builder)
        {
            builder.HasKey(c => c.Id);

            builder.OwnsOne(a => a.Title, title =>
            {
                title.Property(t => t.Text)
                  .HasColumnName("Title")
                  .IsRequired().HasMaxLength(50).HasColumnType("nchar(50)");
            });

            builder.OwnsOne(a => a.ImageURI, title =>
            {
                title.Property(t => t.Path)
                  .HasColumnName("ImageURI")
                  .IsRequired().HasMaxLength(50).HasColumnType("nchar(255)");
            });
        }
    }
}
