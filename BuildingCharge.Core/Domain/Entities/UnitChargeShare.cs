using BuildingCharge.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Domain.Entities
{
    public class UnitChargeShare: EntityBase
    {
        public int ChargeId { get; set; }
        public int UnitId { get; set; }

        // ضریب اختصاصی این واحد برای این شارژ
        public decimal Coefficient { get; set; }

        // مبلغ سهم محاسبه‌شده از شارژ (بدون تراز قبلی)
        public decimal CalculatedAmount { get; set; }

        // مبلغ نهایی پس از لحاظ PreviousBalance (Read Model ذخیره‌ای)
        public decimal FinalAmount { get; set; }

        public Unit Unit { get; set; } = default!;
        public Charge Charge { get; set; } = default!;
    }
}
