using BuildingCharge.Core.Application.DTOs.Charges;
using Swashbuckle.AspNetCore.Filters;

namespace BuildingCharge.WebAPI.SwaggerExamples.Charges
{
    public class CleaningChargeExample : IExamplesProvider<CreateChargeDto>
    {
        public CreateChargeDto GetExamples()
        {
            return new CreateChargeDto
            {
                Type = "Cleaning",
                SourceType = 3, // Manual
                Period = new DateTime(2025, 9, 1),
                Items = new List<CreateChargeItemDto>
            {
                new CreateChargeItemDto { Description = "نظافت اول شهریور", Amount = 700000 },
                new CreateChargeItemDto { Description = "نظافت دوم شهریور", Amount = 1100000 },
                new CreateChargeItemDto { Description = "نظافت سوم شهریور", Amount = 800000 },
                new CreateChargeItemDto { Description = "نظافت چهارم شهریور", Amount = 950000 }
            },
                Shares = Enumerable.Range(1, 9)
                    .Select(id => new CreateShareDto { UnitId = id, Coefficient = 1 })
                    .ToList()
            };
        }
    }

}
