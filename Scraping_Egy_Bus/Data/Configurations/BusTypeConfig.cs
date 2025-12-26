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
    public class BusTypeConfig : IEntityTypeConfiguration<BusType>
    {
        public void Configure(EntityTypeBuilder<BusType> builder)
        {
            builder.HasKey(e => e.BusTypeId);

            builder.Property(e => e.TypeName)
                .IsRequired(false)
                .HasMaxLength(100);

            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.UpdatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

            // Relationship: Company → BusTypes (partial)
            builder.HasOne(bt => bt.Company)
                .WithMany(c => c.BusTypes)
                .HasForeignKey(bt => bt.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(e => e.CompanyId)
              .HasDatabaseName("IX_BusType_Company");


            builder.HasIndex(e => e.TypeName)
                .HasDatabaseName("IX_BusType_Name");

        }
    }
}
