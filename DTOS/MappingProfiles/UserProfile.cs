using AutoMapper;
using EcoLudicoAPI.DTOS;
using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<LoginRequestDTO, User>(); 
        }
    }
}
