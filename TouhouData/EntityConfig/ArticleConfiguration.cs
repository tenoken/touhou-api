using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TouhouArticleMaker.Domain;

namespace TouhouData.EntityConfig
{
    public class ArticleConfiguration : IEntityTypeConfiguration<Article>
    {

        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);

            builder.OwnsOne(a => a.Title, title => 
                { title.Property(t => t.Text)
                    .HasColumnName("Title")
                    .IsRequired().HasMaxLength(50).HasColumnType("nchar(50)"); 
                });

            builder.OwnsOne(a => a.Intro, intro => 
                { intro.Property(i => i.Text)
                    .HasColumnName("Intro")
                    .IsRequired()
                    .HasMaxLength(10000).HasColumnType("nvarchar(max)"); 
                });

            builder.Ignore(a => a.Sections);

            builder.Property(a => a.Category).IsRequired().HasMaxLength(2);

        }
    }
}
