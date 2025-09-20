using AutoMapper;
using BuildingCharge.Core.Application.DTOs;
using BuildingCharge.Core.Application.DTOs.Charges;
using BuildingCharge.Core.Application.Exceptions;
using BuildingCharge.Core.Application.Interfaces;
using BuildingCharge.Core.Domain.Entities;
using BuildingCharge.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.Services
{
    public class ChargeService : IChargeService
    {
        private readonly IChargeRepository _chargeRepo;
        private readonly IUnitRepository _unitRepo;
        private readonly IMapper _mapper;
        public ChargeService(IChargeRepository chargeRepo, IUnitRepository unitRepo, IMapper mapper)
        {
            _chargeRepo = chargeRepo;
            _unitRepo = unitRepo;
            _mapper = mapper;
        }


        // without pagination
        public async Task<List<UnitChargesReportDto>> GetUnitBasedChargesReportAsync(CancellationToken ct = default)
        {
            // همه شارژها رو با Shares و Items لود می‌کنیم
            var charges = await _chargeRepo.GetAllWithSharesAndItemsAsync(ct);

            // همه واحدهایی که در این شارژها سهم دارند
            var allUnitIds = charges
                .SelectMany(c => c.Shares.Select(s => s.UnitId))
                .Distinct()
                .ToList();

            // اطلاعات واحدها
            var units = await _unitRepo.GetByIdsAsync(allUnitIds, ct);

            var report = new List<UnitChargesReportDto>();

            foreach (var unit in units)
            {
                var row = new UnitChargesReportDto
                {
                    UnitId = unit.Id,
                    UnitName = unit.Name,
                    Debt = unit.TotalDebt,
                    Credit = unit.TotalCredit
                };

                // برای هر شارژ، سهم این واحد رو محاسبه می‌کنیم
                foreach (var charge in charges)
                {
                    var share = charge.Shares.FirstOrDefault(s => s.UnitId == unit.Id);
                    if (share == null)
                    {
                        row.Charges[charge.Type] = new ChargeDetailDto
                        {
                            Coefficient = null,
                            FinalAmount = 0
                        };
                        continue;
                    }

                    var totalCoefficient = charge.Shares.Sum(s => s.Coefficient);
                    bool hasCoefficient = charge.Type == "آب" || charge.Type == "گاز";

                    decimal baseAmount;
                    if (hasCoefficient)
                        baseAmount = (share.Coefficient / totalCoefficient) * charge.TotalAmount;
                    else
                        baseAmount = charge.TotalAmount / charge.Shares.Count;

                    row.Charges[charge.Type] = new ChargeDetailDto
                    {
                        Coefficient = hasCoefficient ? share.Coefficient : null,
                        FinalAmount = Math.Round(baseAmount, 0)
                    };
                }

                // محاسبه مبلغ کل قابل پرداخت
                var sumCharges = row.Charges.Values.Sum(c => c.FinalAmount);
                row.PayableAmount = sumCharges + row.Credit - row.Debt;

                report.Add(row);
            }

            return report;
        }

        public async Task<PagedResult<UnitChargesReportDto>> GetUnitBasedChargesReportAsync(
             int pageNumber = 1, int pageSize = 20, CancellationToken ct = default)
        {
            var charges = await _chargeRepo.GetAllWithSharesAndItemsAsync(ct);

            var (units, totalCount) = await _unitRepo.GetPagedUnitsAsync(pageNumber, pageSize, ct);

            var report = new List<UnitChargesReportDto>();

            foreach (var unit in units)
            {
                var row = new UnitChargesReportDto
                {
                    UnitId = unit.Id,
                    UnitName = unit.Name,
                    Debt = unit.TotalDebt,
                    Credit = unit.TotalCredit
                };

                foreach (var charge in charges)
                {
                    var share = charge.Shares.FirstOrDefault(s => s.UnitId == unit.Id);
                    if (share == null)
                    {
                        row.Charges[charge.Type] = new ChargeDetailDto
                        {
                            Coefficient = null,
                            FinalAmount = 0
                        };
                        continue;
                    }

                    var totalCoefficient = charge.Shares.Sum(s => s.Coefficient);
                    bool hasCoefficient = charge.Type == "آب" || charge.Type == "گاز";

                    decimal baseAmount;
                    if (hasCoefficient)
                        baseAmount = (share.Coefficient / totalCoefficient) * charge.TotalAmount;
                    else
                        baseAmount = charge.TotalAmount / charge.Shares.Count;

                    row.Charges[charge.Type] = new ChargeDetailDto
                    {
                        Coefficient = hasCoefficient ? share.Coefficient : null,
                        FinalAmount = Math.Round(baseAmount, 0)
                    };
                }

                var sumCharges = row.Charges.Values.Sum(c => c.FinalAmount);
                row.PayableAmount = sumCharges + row.Credit - row.Debt;

                report.Add(row);
            }

            return new PagedResult<UnitChargesReportDto>
            {
                Items = report,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }


        public async Task<List<ChargeWithUnitsDto>> GetAllChargesReportAsync(CancellationToken ct = default)
        {
            var charges = await _chargeRepo.GetAllWithSharesAndItemsAsync(ct);

            var result = new List<ChargeWithUnitsDto>();

            foreach (var charge in charges)
            {
                var units = await _unitRepo.GetByIdsAsync(charge.Shares.Select(s => s.UnitId), ct);
                var totalCoefficient = charge.Shares.Sum(s => s.Coefficient);
                bool hasCoefficient = charge.Type == "آب" || charge.Type == "گاز";

                var unitList = charge.Shares.Select(s =>
                {
                    var unit = units.First(u => u.Id == s.UnitId);

                    decimal baseAmount;
                    if (hasCoefficient)
                        baseAmount = (s.Coefficient / totalCoefficient) * charge.TotalAmount;
                    else
                        baseAmount = charge.TotalAmount / units.Count;

                    var finalAmount = baseAmount + unit.PreviousBalance;

                    return new UnitChargeListDto
                    {
                        UnitId = unit.Id,
                        UnitName = unit.Name,
                        Debt = unit.TotalDebt,
                        Credit = unit.TotalCredit,
                        PreviousBalance = unit.PreviousBalance,
                        Coefficient = hasCoefficient ? s.Coefficient : null,
                        SharePercent = hasCoefficient ? Math.Round((s.Coefficient / totalCoefficient) * 100, 2) : null,
                        ChargeUnitShare = Math.Round(baseAmount, 0),
                        FinalAmount = Math.Round(finalAmount, 0)
                    };
                }).ToList();

                result.Add(new ChargeWithUnitsDto
                {
                    ChargeId = charge.Id,
                    ChargeType = charge.Type,
                    TotalAmount = charge.TotalAmount,
                    Period = charge.Period,
                    Units = unitList
                });
            }

            return result;
        }


        public async Task DeleteAsync(int id, CancellationToken ct)
        {
            var charge = await _chargeRepo.GetByIdFullAsync(id, ct); // شامل Items و Shares
            if (charge == null)
                throw new NotFoundException($"Charge {id} not found.");

            var hasFinalizedShares = charge.Shares.Any(s => s.FinalAmount.HasValue);
            if (hasFinalizedShares)
                throw new InvalidOperationException("This charge has finalized shares and cannot be deleted.");

            await _chargeRepo.DeleteAsync(charge, ct);
        }



        public async Task<ChargeResponseDto> CreateAsync(CreateChargeDto dto, CancellationToken ct)
        {
            var charge = _mapper.Map<Charge>(dto);
            var created = await _chargeRepo.AddAsync(charge, ct);
            return _mapper.Map<ChargeResponseDto>(created);
        }


    }
}
