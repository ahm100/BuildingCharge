using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.DTOs.Charges
{
    public class CreateChargeDto
    {
        public string Type { get; set; } = default!;
        public int SourceType { get; set; }
        public DateTime Period { get; set; }
        public decimal? ManualAmount { get; set; }

        public List<CreateChargeItemDto> Items { get; set; } = new();
        public List<CreateShareDto> Shares { get; set; } = new();
    }

    public class CreateChargeItemDto
    {
        public string Description { get; set; } = default!;
        public decimal Amount { get; set; }
    }

    public class CreateShareDto
    {
        public int UnitId { get; set; }
        public decimal Coefficient { get; set; }
    }

}
