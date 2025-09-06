using BuildingCharge.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Domain.Entities
{
    public class Unit : EntityBase
    {
        public string Name { get; set; } = default!;
        public decimal TotalDebt { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal PreviousBalance => TotalCredit - TotalDebt;

        public ICollection<UnitChargeShare> Shares { get; set; } = new List<UnitChargeShare>();
    }
}
