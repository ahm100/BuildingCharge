using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.DTOs.Charges
{
    public class ChargeResponseDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = default!;
        public int SourceType { get; set; }
        public DateTime Period { get; set; }
        public decimal? ManualAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public List<ChargeItemDto> Items { get; set; } = new();
        public List<ShareDto> Shares { get; set; } = new();
    }

    public class ChargeItemDto
    {
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public decimal Amount { get; set; }
    }

    public class ShareDto
    {
        public int UnitId { get; set; }
        public decimal Coefficient { get; set; }
        public decimal? CalculatedAmount { get; set; }
        public decimal? FinalAmount { get; set; }
    }
}
