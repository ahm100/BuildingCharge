using BuildingCharge.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.DTOs
{
    public class ChargeCreateDto
    {
        public string Type { get; set; } = default!;
        public ChargeSource SourceType { get; set; }
        public DateTime Period { get; set; }
        public decimal? ManualAmount { get; set; }
        public List<ChargeItemDto>? Items { get; set; }
    }

    public class ChargeItemDto
    {
        public string Description { get; set; } = default!;
        public decimal Amount { get; set; }
    }
}
