using BuildingCharge.Core.Application.Interfaces;
using BuildingCharge.Core.Domain.Entities;
using BuildingCharge.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Infrastructure.Repositories
{
    public class ChargeItemRepository : RepositoryBase<ChargeItem>, IChargeItemRepository
    {
        public ChargeItemRepository(AppDbContext db) : base(db) { }

        public async Task<List<ChargeItem>> GetByChargeIdAsync(int chargeId, CancellationToken ct = default)
            => await _db.ChargeItems
                .Where(i => i.ChargeId == chargeId)
                .AsNoTracking()
                .ToListAsync(ct);
    }
}
