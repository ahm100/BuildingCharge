using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.DTOs
{
    public class ChargeWithUnitsDto
    {
        public int ChargeId { get; set; }
        public string ChargeType { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Period { get; set; }
        public List<UnitChargeListDto> Units { get; set; } = new();
    }

    public class UnitChargeListDto
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal Debt { get; set; }
        public decimal Credit { get; set; }
        public decimal PreviousBalance { get; set; }
        public decimal? Coefficient { get; set; }     // فقط برای آب و گاز
        public decimal? SharePercent { get; set; }    // فقط برای آب و گاز
        public decimal ChargeUnitShare { get; set; }
        public decimal FinalAmount { get; set; }
    }

}
