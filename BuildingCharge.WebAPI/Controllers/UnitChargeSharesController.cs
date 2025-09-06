using BuildingCharge.Core.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BuildingCharge.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitChargeSharesController : ControllerBase
    {
        private readonly IUnitChargeShareRepository _shareRepo;

        public UnitChargeSharesController(IUnitChargeShareRepository shareRepo)
        {
            _shareRepo = shareRepo;
        }

        [HttpGet("charge/{chargeId:int}")]
        public async Task<IActionResult> GetByCharge(int chargeId, CancellationToken ct)
        {
            var shares = await _shareRepo.GetByChargeIdAsync(chargeId, ct);
            return Ok(shares);
        }
    }
}
