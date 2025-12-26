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

    public class AdditionalTripDataConfig : IEntityTypeConfiguration<AdditionalTripData>
    {
        public void Configure(EntityTypeBuilder<AdditionalTripData> builder)
        {
            builder.HasKey(e => e.TripDataId);

            builder.Property(e => e.Data)
               .IsRequired(false)
               .HasColumnType("nvarchar(max)");

            builder.Property(e => e.Features)
                .IsRequired(false)
                .HasColumnType("nvarchar(max)");


            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.UpdatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

            // Relationship: Trip → AdditionalTripData
            builder.HasOne(atd => atd.Trip)
                .WithMany(t => t.AdditionalData)
                .HasForeignKey(atd => atd.TripId)
                .OnDelete(DeleteBehavior.Cascade);



            builder.HasIndex(e => e.TripId)
                .HasDatabaseName("IX_AdditionalTripData_Trip");

        }
    }
}
