using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TouhouArticleMaker.Domain;

namespace TouhouData.EntityConfig
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(s => s.Id);

            builder.OwnsOne(s => s.Name, title => 
            { 
                title.Property(t => t.FirstName)
                .HasColumnName("FirstName")
                .HasColumnType("nchar(50)"); 
            });

            builder.OwnsOne(s => s.Name, title => 
            { 
                title.Property(t => t.LastName)
                .HasColumnName("LastName")
                .HasColumnType("nchar(50)"); 
            });

            builder.OwnsOne(s => s.UserName, title => 
            { 
                title.Property(t => t.Text); 
            });

            builder.OwnsOne(s => s.Email, title => 
            { 
                title.Property(t => t.Address)
                //.HasColumnName("Email")
                .HasColumnType("nchar(50)");
            });
        }
    }
}
