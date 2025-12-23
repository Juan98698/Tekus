using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekus.Domain.Entities;

namespace Tekus.Infrastructure.Persistence.Context
{
    public class TekusDbContext : DbContext
    {
        public TekusDbContext(DbContextOptions<TekusDbContext> options)
            : base(options) { }

        public DbSet<Provider> Providers => Set<Provider>();

        public DbSet<Service> Services => Set<Service>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TekusDbContext).Assembly);
        }
    }
}
