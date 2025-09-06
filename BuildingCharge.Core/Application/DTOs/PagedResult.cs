using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.DTOs
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; } // تعداد کل رکوردها
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

}
