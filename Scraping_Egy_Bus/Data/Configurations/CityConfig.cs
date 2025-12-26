using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scraping_Egy_Bus.Entities;

namespace Scraping_Egy_Bus.Configurations
{
    public class CityConfig : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(e => e.CityId);

            builder.Property(e => e.CityName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.UpdatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

            // Index for fast searching
            // builder.HasIndex(e => e.CityName);

            builder.HasIndex(e => e.CityName)
                .IsUnique()
                .HasDatabaseName("UX_City_Name");
        }
    }
}
