using AutoMapper;
using EcoLudicoAPI.DTOS;
using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.MappingProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDTO>().ReverseMap();
        }
    }
}
