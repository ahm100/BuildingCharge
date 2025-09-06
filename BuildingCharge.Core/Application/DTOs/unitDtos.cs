using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.DTOs
{
    public class UnitCreateDto
    {
        public string Name { get; set; } = default!;
        public decimal TotalDebt { get; set; }
        public decimal TotalCredit { get; set; }
    }

    public class ShareInputDto
    {
        public Guid ChargeId { get; set; }
        public Guid UnitId { get; set; }
        public decimal Coefficient { get; set; }
    }
}
