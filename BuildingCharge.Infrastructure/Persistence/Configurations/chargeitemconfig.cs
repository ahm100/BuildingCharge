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
    public class ChargeItemConfig : IEntityTypeConfiguration<ChargeItem>
    {
        public void Configure(EntityTypeBuilder<ChargeItem> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Description).HasMaxLength(200);
            b.Property(x => x.Amount).HasColumnType("decimal(18,2)");
        }
    }
}
