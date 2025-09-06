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
    public class UnitRepository : RepositoryBase<Unit>, IUnitRepository
    {
        public UnitRepository(AppDbContext db) : base(db) { }

        public override async Task<List<Unit>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default)
            => await _db.Units.Where(u => ids.Contains(u.Id)).AsNoTracking().ToListAsync(ct);

        public Task<(List<Unit> Items, int TotalCount)> GetPagedUnitsAsync(
        int pageNumber, int pageSize, CancellationToken ct = default)
        {
            return GetPagedAsync(pageNumber, pageSize, null, q => q.OrderBy(u => u.Name), ct);
        }
    }
}
