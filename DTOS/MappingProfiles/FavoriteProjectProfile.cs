using AutoMapper;
using EcoLudicoAPI.DTOS;
using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.MappingProfiles
{
    public class FavoriteProjectProfile : Profile
    {
        public FavoriteProjectProfile()
        {
            CreateMap<FavoriteProject, FavoriteProjectDTO>().ReverseMap();
        }
    }
}
