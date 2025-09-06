using BuildingCharge.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.Interfaces
{
    public interface IUnitChargeShareRepository : IRepositoryBase<UnitChargeShare>
    {
        Task<List<UnitChargeShare>> GetByChargeIdAsync(int chargeId, CancellationToken ct = default);
        Task<List<UnitChargeShare>> GetByChargeAndUnitIdsAsync(int chargeId, IEnumerable<int> unitIds, CancellationToken ct = default);
    }
}
