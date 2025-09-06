using BuildingCharge.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.Services
{
    public interface IChargeCalculator
    {
        List<UnitChargeShare> CalculateShares(
            Charge charge,
            List<Unit> units,
            List<(int UnitId, decimal Coefficient)> inputs);
    }
}
