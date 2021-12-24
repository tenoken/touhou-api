using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TouhouArticleMaker.Domain;

namespace TouhouData.EntityConfig
{
    public class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.HasKey(s => s.Id);

            builder.OwnsOne(s => s.Title, title => { title.Property(t => t.Text); });

            builder.OwnsOne(s => s.Text, text => { text.Property(t => t.TextContent); });
        }
    }
}
