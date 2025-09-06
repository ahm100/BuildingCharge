using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Domain.Enums
{
    public enum ChargeSource
    {
        Bill = 0,     // قبض
        Invoice = 1,  // فاکتور
        Advance = 2,  // علی‌الحساب
        Manual = 3    // دستی/ثابت
    }
}
