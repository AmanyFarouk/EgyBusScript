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
    public class StationConfig : IEntityTypeConfiguration<Station>
    {
        public void Configure(EntityTypeBuilder<Station> builder)
        {
            builder.HasKey(e => e.StationId);

            builder.Property(e => e.StationName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Address)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.UpdatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

            // Relationships...
            builder.HasOne(s => s.City)
                .WithMany(c => c.Stations)
                .HasForeignKey(s => s.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Company)
                .WithMany(c => c.Stations)
                .HasForeignKey(s => s.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => new { e.CityId, e.CompanyId })
            .HasDatabaseName("IX_Station_City_Company");

            builder.HasIndex(e => e.StationName)
                .HasDatabaseName("IX_Station_Name");


        }
    }
}
