using BuildingCharge.Core.Application.DTOs.Charges;
using Swashbuckle.AspNetCore.Filters;

namespace BuildingCharge.WebAPI.SwaggerExamples.Charges
{

    public class WaterChargeExample : IExamplesProvider<CreateChargeDto>
    {
        public CreateChargeDto GetExamples()
        {
            return new CreateChargeDto
            {
                Type = "Water",
                SourceType = 0, // Bill
                Period = new DateTime(2025, 9, 1),
                Items = new List<CreateChargeItemDto>
            {
                new CreateChargeItemDto { Description = "قبض آب شهریور 1404", Amount = 1429500 }
            },
                Shares = new List<CreateShareDto>
            {
                new CreateShareDto { UnitId = 1, Coefficient = 1 },
                new CreateShareDto { UnitId = 2, Coefficient = 1 },
                new CreateShareDto { UnitId = 3, Coefficient = 0.2m },
                new CreateShareDto { UnitId = 4, Coefficient = 1 },
                new CreateShareDto { UnitId = 5, Coefficient = 0.8m },
                new CreateShareDto { UnitId = 6, Coefficient = 1 },
                new CreateShareDto { UnitId = 7, Coefficient = 1 },
                new CreateShareDto { UnitId = 8, Coefficient = 1 },
                new CreateShareDto { UnitId = 9, Coefficient = 1 }
            }
            };
        }
    }

}
