using BuildingCharge.Core.Application.DTOs;
using BuildingCharge.Core.Application.DTOs.Charges;
using BuildingCharge.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.Services
{
    public interface IChargeService
    {
        Task<List<UnitChargesReportDto>> GetUnitBasedChargesReportAsync(CancellationToken ct = default);

        // with pagination
        Task<PagedResult<UnitChargesReportDto>> GetUnitBasedChargesReportAsync(
            int pageNumber = 1, int pageSize = 20, CancellationToken ct = default);

         Task<List<ChargeWithUnitsDto>> GetAllChargesReportAsync(CancellationToken ct = default);

        Task DeleteAsync(int id, CancellationToken ct);

        Task<ChargeResponseDto> CreateAsync(CreateChargeDto dto, CancellationToken ct);


    }
}
