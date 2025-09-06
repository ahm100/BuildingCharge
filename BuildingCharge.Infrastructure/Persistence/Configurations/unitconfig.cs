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
    public class UnitConfig : IEntityTypeConfiguration<Unit>
    {
        public void Configure(EntityTypeBuilder<Unit> b)
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired().HasMaxLength(100);
            b.Property(x => x.TotalDebt).HasColumnType("decimal(18,2)");
            b.Property(x => x.TotalCredit).HasColumnType("decimal(18,2)");
            // PreviousBalance فقط getter است و مپ نمی‌شود
            b.Ignore(x => x.PreviousBalance);
        }
    }
}
