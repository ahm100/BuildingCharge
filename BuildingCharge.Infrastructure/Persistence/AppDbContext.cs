using BuildingCharge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Unit> Units => Set<Unit>();
        public DbSet<Charge> Charges => Set<Charge>();
        public DbSet<ChargeItem> ChargeItems => Set<ChargeItem>();
        public DbSet<UnitChargeShare> UnitChargeShares => Set<UnitChargeShare>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
