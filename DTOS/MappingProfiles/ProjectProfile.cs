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
            CreateMap<ProjectCreateDTO, Project>().ReverseMap();
            CreateMap<ProjectUpdateDTO, Project>().ReverseMap();
        }
    }
}
