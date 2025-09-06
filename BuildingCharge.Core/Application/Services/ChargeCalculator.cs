using BuildingCharge.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.Services
{
    public class ChargeCalculator : IChargeCalculator
    {
        public List<UnitChargeShare> CalculateShares(
            Charge charge,
            List<Unit> units,
            List<(int UnitId, decimal Coefficient)> inputs)
        {
            var totalCoefficient = inputs.Sum(i => i.Coefficient);
            if (totalCoefficient <= 0) throw new InvalidOperationException("جمع ضرایب باید بزرگ‌تر از صفر باشد.");

            var result = new List<UnitChargeShare>();

            foreach (var input in inputs)
            {
                var unit = units.First(u => u.Id == input.UnitId);
                var baseAmount = (input.Coefficient / totalCoefficient) * charge.TotalAmount;
                var finalAmount = baseAmount + unit.PreviousBalance;

                result.Add(new UnitChargeShare
                {
                    ChargeId = charge.Id,
                    UnitId = unit.Id,
                    Coefficient = input.Coefficient,
                    CalculatedAmount = Math.Round(baseAmount, 0),
                    FinalAmount = Math.Round(finalAmount, 0)
                });
            }

            return result;
        }
    }
}
