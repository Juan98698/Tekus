using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;

namespace Tekus.Infrastructure.Persistence.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                   .HasMaxLength(32)
                   .IsRequired();

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(s => s.HourValueUsd)
                   .HasPrecision(18, 2);

            // Owned Value Object: Countries
            builder.OwnsMany(s => s.Countries, cb =>
            {
                cb.ToTable("ServiceCountries");

                cb.WithOwner().HasForeignKey("ServiceId");

                cb.Property(c => c.Code)
                  .HasMaxLength(10)
                  .IsRequired();

                cb.Property(c => c.Name)
                  .HasMaxLength(100)
                  .IsRequired();

                cb.HasKey("ServiceId", "Code");
            });
        }
    }
}
