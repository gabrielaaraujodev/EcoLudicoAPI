using AutoMapper;
using EcoLudicoAPI.DTOS;
using EcoLudicoAPI.Models;
using System.Linq;

namespace EcoLudicoAPI.MappingProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDTO>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.ImageUrls.Select(i => i.Url))).ReverseMap();

            CreateMap<ProjectDTO, Project>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src =>
                    src.ImageUrls.Select(url => new ImageUrl { Url = url }).ToList()
                ));


            CreateMap<ProjectCreateDTO, Project>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src =>
                    src.ImageUrls.Select(url => new ImageUrl { Url = url }).ToList()
                )).ReverseMap();

            CreateMap<ProjectUpdateDTO, Project>().ReverseMap();
        }
    }
}
