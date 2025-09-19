using BuildingCharge.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Domain.Entities
{
    public class ChargeItem : EntityBase
    {
        public int ChargeId { get; set; }
        public string? Description { get; set; }
        public decimal Amount { get; set; }

        public Charge? Charge { get; set; }
    }
}
