using BuildingCharge.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Infrastructure.Persistence.Configurations
{
    public class UnitChargeShareConfig : IEntityTypeConfiguration<UnitChargeShare>
    {
        public void Configure(EntityTypeBuilder<UnitChargeShare> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Coefficient).HasColumnType("decimal(18,6)");
            b.Property(x => x.CalculatedAmount).HasColumnType("decimal(18,2)");
            b.Property(x => x.FinalAmount).HasColumnType("decimal(18,2)");

            b.HasOne(x => x.Unit)
             .WithMany(u => u.Shares)
             .HasForeignKey(x => x.UnitId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Charge)
             .WithMany(c => c.Shares)
             .HasForeignKey(x => x.ChargeId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
