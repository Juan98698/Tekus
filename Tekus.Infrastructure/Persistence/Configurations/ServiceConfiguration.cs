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

            // Value Object Country as owned collection
            builder.OwnsMany(s => s.Countries, c =>
            {
                c.Property(x => x.Code).HasMaxLength(10);
                c.Property(x => x.Name).HasMaxLength(100);
            });
        }
    }
}
