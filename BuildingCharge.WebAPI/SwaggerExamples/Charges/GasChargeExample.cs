using BuildingCharge.Core.Application.DTOs.Charges;
using Swashbuckle.AspNetCore.Filters;

namespace BuildingCharge.WebAPI.SwaggerExamples.Charges
{
    public class GasChargeExample : IExamplesProvider<CreateChargeDto>
    {
        public CreateChargeDto GetExamples()
        {
            return new CreateChargeDto
            {
                Type = "Gas",
                SourceType = 0, // Bill
                Period = new DateTime(2025, 9, 1),
                Items = new List<CreateChargeItemDto>
            {
                new CreateChargeItemDto { Description = "قبض گاز شهریور 1404", Amount = 1950000 }
            },
                Shares = new List<CreateShareDto>
            {
                new CreateShareDto { UnitId = 1, Coefficient = 1 },
                new CreateShareDto { UnitId = 2, Coefficient = 1 },
                new CreateShareDto { UnitId = 3, Coefficient = 0.3m },
                new CreateShareDto { UnitId = 4, Coefficient = 1 },
                new CreateShareDto { UnitId = 5, Coefficient = 1 },
                new CreateShareDto { UnitId = 6, Coefficient = 1 },
                new CreateShareDto { UnitId = 7, Coefficient = 1 },
                new CreateShareDto { UnitId = 8, Coefficient = 1 },
                new CreateShareDto { UnitId = 9, Coefficient = 1.5m }
            }
            };
        }
    }

}
