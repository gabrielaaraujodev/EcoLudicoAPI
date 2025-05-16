using AutoMapper;
using EcoLudicoAPI.DTOS;
using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.MappingProfiles
{
    public class SchoolProfile : Profile
    {
        public SchoolProfile()
        {
            CreateMap<School, SchoolDTO>().ReverseMap();
            CreateMap<SchoolCreateDTO, School>().ReverseMap();
            CreateMap<SchoolUpdateDTO, School>()
            .ForMember(dest => dest.Address, opt => opt.Ignore());
            CreateMap<AddressDTO, Address>();
        }
    }
}
