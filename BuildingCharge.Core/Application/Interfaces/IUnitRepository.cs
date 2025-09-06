using BuildingCharge.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.Interfaces
{
    public interface IUnitRepository : IRepositoryBase<Unit>
    {
        Task<List<Unit>> GetByIdsAsync(IEnumerable<int> ids, CancellationToken ct = default);
        Task<(List<Unit> Items, int TotalCount)> GetPagedUnitsAsync(
        int pageNumber, int pageSize, CancellationToken ct = default);

    }
}
