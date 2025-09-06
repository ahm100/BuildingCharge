using BuildingCharge.Core.Domain.Common;
using BuildingCharge.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Domain.Entities
{
    public class Charge : EntityBase
    {
        public string Type { get; set; } = default!; // "آب"، "برق"، "نظافت"، ...
        public ChargeSource SourceType { get; set; }
        public DateTime Period { get; set; }

        // مبلغ دستی برای حالت Manual/Advance
        public decimal? ManualAmount { get; set; }

        public ICollection<ChargeItem> Items { get; set; } = new List<ChargeItem>();

        // اگر آیتم هست: جمع آیتم‌ها، وگرنه ManualAmount (یا 0)
        public decimal TotalAmount => Items != null && Items.Count > 0
            ? Items.Sum(i => i.Amount)
            : (ManualAmount ?? 0m);

        public ICollection<UnitChargeShare> Shares { get; set; } = new List<UnitChargeShare>();
    }
}
