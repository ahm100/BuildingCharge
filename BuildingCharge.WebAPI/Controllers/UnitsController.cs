using BuildingCharge.Core.Application.Interfaces;
using BuildingCharge.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BuildingCharge.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitRepository _unitRepo;

        public UnitsController(IUnitRepository unitRepo)
        {
            _unitRepo = unitRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            var units = await _unitRepo.GetAllAsync(ct);
            return Ok(units);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
        {
            var unit = await _unitRepo.GetByIdAsync(id, ct);
            if (unit == null) return NotFound();
            return Ok(unit);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Unit unit, CancellationToken ct)
        {
            var created = await _unitRepo.AddAsync(unit, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Unit unit, CancellationToken ct)
        {
            var existing = await _unitRepo.GetByIdAsync(id, ct);
            if (existing == null) return NotFound();

            existing.Name = unit.Name;
            existing.TotalDebt = unit.TotalDebt;
            existing.TotalCredit = unit.TotalCredit;

            await _unitRepo.UpdateAsync(existing, ct);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var existing = await _unitRepo.GetByIdAsync(id, ct);
            if (existing == null) return NotFound();

            await _unitRepo.DeleteAsync(existing, ct);
            return NoContent();
        }
    }
}
