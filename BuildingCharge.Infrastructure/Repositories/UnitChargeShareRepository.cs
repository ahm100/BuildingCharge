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
    public class UnitChargeShareRepository : RepositoryBase<UnitChargeShare>, IUnitChargeShareRepository
    {
        public UnitChargeShareRepository(AppDbContext db) : base(db) { }

        public async Task<List<UnitChargeShare>> GetByChargeIdAsync(int chargeId, CancellationToken ct = default)
            => await _db.UnitChargeShares
                .Where(s => s.ChargeId == chargeId)
                .AsNoTracking()
                .ToListAsync(ct);

        public async Task<List<UnitChargeShare>> GetByChargeAndUnitIdsAsync(int chargeId, IEnumerable<int> unitIds, CancellationToken ct = default)
            => await _db.UnitChargeShares
                .Where(s => s.ChargeId == chargeId && unitIds.Contains(s.UnitId))
                .AsNoTracking()
                .ToListAsync(ct);
    }
}
