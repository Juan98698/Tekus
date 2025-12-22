using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;
using System.Text.Json;




namespace Tekus.Infrastructure.Persistence.Configurations
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(p => p.Nit)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(p => p.Email)
                   .IsRequired()
                   .HasMaxLength(200);


            var customFieldsConverter =
            new ValueConverter<Dictionary<string, string>, string>(
           v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
           v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions?)null)!
            );

            var customFieldsComparer =
            new ValueComparer<Dictionary<string, string>>(
            (d1, d2) => d1!.SequenceEqual(d2!),
             d => d.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key, v.Value)),
             d => d.ToDictionary(e => e.Key, e => e.Value)
             );
           
            
            var customFieldsProperty = builder
            .Property<Dictionary<string, string>>("_customFields")
            .HasColumnName("CustomFields")
            .HasConversion(customFieldsConverter)
            .HasColumnType("nvarchar(max)");

            customFieldsProperty.Metadata
                .SetValueComparer(customFieldsComparer);



            builder.HasMany(p => p.Services)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
