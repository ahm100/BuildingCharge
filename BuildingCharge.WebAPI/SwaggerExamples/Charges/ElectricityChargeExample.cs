using BuildingCharge.Core.Application.DTOs.Charges;
using Swashbuckle.AspNetCore.Filters;

namespace BuildingCharge.WebAPI.SwaggerExamples.Charges
{
    public class ElectricityChargeExample : IExamplesProvider<CreateChargeDto>
    {
        public CreateChargeDto GetExamples()
        {
            return new CreateChargeDto
            {
                Type = "Electricity",
                SourceType = 0, // Bill
                Period = new DateTime(2025, 9, 1),
                Items = new List<CreateChargeItemDto>
            {
                new CreateChargeItemDto { Description = "قبض برق شهریور 1404", Amount = 875000 }
            },
                Shares = Enumerable.Range(1, 9)
                    .Select(id => new CreateShareDto { UnitId = id, Coefficient = 1 })
                    .ToList()
            };
        }
    }

}
