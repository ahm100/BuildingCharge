using BuildingCharge.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.Interfaces
{
    public interface IChargeItemRepository : IRepositoryBase<ChargeItem>
    {
        Task<List<ChargeItem>> GetByChargeIdAsync(int chargeId, CancellationToken ct = default);
    }
}
