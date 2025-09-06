using BuildingCharge.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.Interfaces
{
    public interface IChargeRepository : IRepositoryBase<Charge>
    {
        Task<List<Charge>> GetAllWithSharesAndItemsAsync(CancellationToken ct = default);

        Task<Charge?> GetByIdWithItemsAsync(int id, CancellationToken ct = default);
        Task<Charge?> GetByIdWithSharesAsync(int id, CancellationToken ct = default);
        Task<Charge?> GetByIdFullAsync(int id, CancellationToken ct = default); // Items + Shares
    }
}
