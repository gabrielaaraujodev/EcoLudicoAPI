using AutoMapper;
using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.DTOS.MappingProfiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
        }
    }
}
