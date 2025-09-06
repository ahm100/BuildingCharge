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
    public class ChargeConfig : IEntityTypeConfiguration<Charge>
    {
        public void Configure(EntityTypeBuilder<Charge> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Type).IsRequired().HasMaxLength(100);
            b.Property(x => x.ManualAmount).HasColumnType("decimal(18,2)");
            b.Ignore(x => x.TotalAmount);

            b.HasMany(x => x.Items)
             .WithOne(i => i.Charge)
             .HasForeignKey(i => i.ChargeId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
