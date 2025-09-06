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
    public class ChargeRepository : RepositoryBase<Charge>, IChargeRepository
    {
        public ChargeRepository(AppDbContext db) : base(db) { }

        public async Task<List<Charge>> GetAllWithSharesAndItemsAsync(CancellationToken ct = default)
        {
            return await _db.Charges
                .Include(c => c.Shares)
                .Include(c => c.Items)
                .AsNoTracking()
                .ToListAsync(ct);
        }


        public async Task<Charge?> GetByIdWithItemsAsync(int id, CancellationToken ct = default)
            => await _db.Charges
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task<Charge?> GetByIdWithSharesAsync(int id, CancellationToken ct = default)
            => await _db.Charges
                .Include(c => c.Shares)
                .FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task<Charge?> GetByIdFullAsync(int id, CancellationToken ct = default)
            => await _db.Charges
                .Include(c => c.Items)
                .Include(c => c.Shares)
                .FirstOrDefaultAsync(c => c.Id == id, ct);
    }
}
