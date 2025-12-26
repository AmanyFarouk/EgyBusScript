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
    public class CompanyConfig : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(e => e.CompanyId);

            builder.Property(e => e.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Website)
                .HasMaxLength(500);

            builder.Property(e => e.PhoneNumber)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(e => e.LogoUrl)
                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(e => e.IsActive)
                .HasDefaultValue(true);

            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(e => e.UpdatedAt)
               .HasDefaultValueSql("GETUTCDATE()");

            builder.HasIndex(e => e.CompanyName)
               .IsUnique()
               .HasDatabaseName("UX_Company_Name");


            builder.HasIndex(e => e.IsActive)
                .HasDatabaseName("IX_Company_Active");
        }
    }
}
