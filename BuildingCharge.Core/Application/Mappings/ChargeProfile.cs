using AutoMapper;
using BuildingCharge.Core.Application.DTOs.Charges;
using BuildingCharge.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingCharge.Core.Application.Mappings
{
    public class ChargeProfile : Profile
    {
        public ChargeProfile()
        {
            CreateMap<CreateChargeDto, Charge>();
            CreateMap<CreateChargeItemDto, ChargeItem>();
            CreateMap<CreateShareDto, UnitChargeShare>();
        }
    }
}
