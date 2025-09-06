using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.DTOs
{
    public class UnitChargesReportDto
    {
        public int UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal Debt { get; set; }
        public decimal Credit { get; set; }
        public Dictionary<string, ChargeDetailDto> Charges { get; set; } = new();
        public decimal PayableAmount { get; set; } // مبلغ کل قابل پرداخت
    }

    public class ChargeDetailDto
    {
        public decimal? Coefficient { get; set; } // فقط برای آب و گاز
        public decimal FinalAmount { get; set; }  // مبلغ نهایی این شارژ
    }


}
