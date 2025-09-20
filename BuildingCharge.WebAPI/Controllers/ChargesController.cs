using BuildingCharge.Core.Application.DTOs.Charges;
using BuildingCharge.Core.Application.Exceptions;
using BuildingCharge.Core.Application.Interfaces;
using BuildingCharge.Core.Application.Services;
using BuildingCharge.Core.Domain.Entities;
using BuildingCharge.Core.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace BuildingCharge.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChargesController : ControllerBase
    {
        private readonly IChargeRepository _chargeRepo;
        private readonly IUnitRepository _unitRepo;
        private readonly IUnitChargeShareRepository _shareRepo;
        private readonly IChargeCalculator _calculator;
        private readonly IChargeService _chargeService;

        public ChargesController(
            IChargeRepository chargeRepo,
            IUnitRepository unitRepo,
            IUnitChargeShareRepository shareRepo,
            IChargeService chargeService,
            IChargeCalculator calculator)
        {
            _chargeRepo = chargeRepo;
            _unitRepo = unitRepo;
            _shareRepo = shareRepo;
            _calculator = calculator;
            _chargeService = chargeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var charges = await _chargeRepo.GetAllAsync(ct);
            return Ok(charges);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var charge = await _chargeRepo.GetByIdFullAsync(id, ct);
            if (charge == null) return NotFound();
            return Ok(charge);
        }


        //[HttpDelete("{id:int}")]
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            try
            {
                await _chargeService.DeleteAsync(id, ct);
                return Ok(new { message = $"Charge {id} deleted successfully." });
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }



        /// <summary>
        /// Creates a new charge and assigns shares to units.
        /// </summary>
        /// <param name="charge">Charge details including type, amount, and share distribution.</param>
        /// <returns>The created charge object.</returns>
        /// <response code="201">Charge successfully created.</response>
        /// <response code="400">Invalid charge data.</response>

        [HttpPost("create")]
        public async Task<IActionResult> CreateFromDto([FromBody] CreateChargeDto dto, CancellationToken ct)
        {
            var result = await _chargeService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }




        [HttpPost("{chargeId:int}/shares")]
        public async Task<IActionResult> UpsertShares(int chargeId, List<UnitChargeShare> shares, CancellationToken ct)
        {
            var existingShares = await _shareRepo.GetByChargeIdAsync(chargeId, ct);
            var existingMap = existingShares.ToDictionary(s => s.UnitId, s => s);

            var toAdd = new List<UnitChargeShare>();
            foreach (var s in shares)
            {
                if (existingMap.TryGetValue(s.UnitId, out var existing))
                {
                    existing.Coefficient = s.Coefficient;
                }
                else
                {
                    s.ChargeId = chargeId;
                    toAdd.Add(s);
                }
            }

            if (toAdd.Any()) await _shareRepo.AddRangeAsync(toAdd, ct);
            if (existingShares.Any()) await _shareRepo.UpdateRangeAsync(existingShares, ct);

            return Ok(new { message = "Shares updated" });
        }

        [HttpPost("{chargeId:int}/calculate")]
        public async Task<IActionResult> Calculate(int chargeId, CancellationToken ct)
        {
            var charge = await _chargeRepo.GetByIdFullAsync(chargeId, ct);
            if (charge == null) return NotFound();

            var units = await _unitRepo.GetByIdsAsync(charge.Shares.Select(s => s.UnitId), ct);
            var inputs = charge.Shares.Select(s => (s.UnitId, s.Coefficient)).ToList();

            var calculated = _calculator.CalculateShares(charge, units, inputs);

            foreach (var calc in calculated)
            {
                var share = charge.Shares.First(s => s.UnitId == calc.UnitId);
                share.CalculatedAmount = calc.CalculatedAmount;
                share.FinalAmount = calc.FinalAmount;
            }

            await _shareRepo.UpdateRangeAsync(charge.Shares, ct);

            return Ok(charge.Shares.Select(s => new
            {
                s.UnitId,
                UnitName = units.First(u => u.Id == s.UnitId).Name,
                s.Coefficient,
                s.CalculatedAmount,
                PreviousBalance = units.First(u => u.Id == s.UnitId).PreviousBalance,
                s.FinalAmount
            }));
        }

        /// <summary>
        /// Retrieves all charges including their unit shares.
        /// </summary>
        /// <returns>List of charges with share details.</returns>
        /// <response code="200">Charges successfully retrieved.</response>

        [HttpGet("all-charges-report")]
        public async Task<IActionResult> GetAllChargesReport(CancellationToken ct)
        {
            var report = await _chargeService.GetAllChargesReportAsync(ct);
            return Ok(report);
        }



        /// <summary>
        /// Generates a financial report for each unit, including charge breakdown, debt, credit, and payable amount.
        /// </summary>
        /// <param name="pageNumber">Page number for pagination.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>Paged report of unit charges.</returns>
        /// <response code="200">Report successfully generated.</response>
        /// <response code="400">Invalid pagination parameters.</response>

        [HttpGet("unit-based-report-NoPaginated")]
        public async Task<IActionResult> GetUnitBasedReportNoPaginated(CancellationToken ct)
        {
            var report = await _chargeService.GetUnitBasedChargesReportAsync(ct);
            return Ok(report);
        }


        /// <summary>
        /// Generates a financial report for each unit, including charge breakdown, debt, credit, and payable amount.
        /// </summary>
        /// <param name="pageNumber">Page number for pagination.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <returns>Paged report of unit charges.</returns>
        /// <response code="200">Report successfully generated.</response>
        /// <response code="400">Invalid pagination parameters.</response>

        [HttpGet("unit-based-report")]
        public async Task<IActionResult> GetUnitBasedReport(
         int pageNumber = 1, int pageSize = 20, CancellationToken ct = default)
        {
            var report = await _chargeService.GetUnitBasedChargesReportAsync(pageNumber, pageSize, ct);
            return Ok(report);
        }


    }
}
