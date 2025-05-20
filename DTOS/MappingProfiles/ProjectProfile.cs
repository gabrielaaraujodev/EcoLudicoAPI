using AutoMapper;
using EcoLudico.DTOS;
using EcoLudicoAPI.DTOS;
using EcoLudicoAPI.Enums;
using EcoLudicoAPI.Models;
using System.Linq;

namespace EcoLudicoAPI.MappingProfiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            CreateMap<Project, ProjectDTO>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => src.ImageUrls.Select(i => i.Url)))
                .ForMember(dest => dest.School, opt => opt.MapFrom(src => src.School));

            CreateMap<ProjectDTO, Project>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src =>
                    src.ImageUrls.Select(url => new ImageUrl { Url = url }).ToList()
                ))
                .ForMember(dest => dest.Comments, opt => opt.Ignore())
                .ForMember(dest => dest.School, opt => opt.Ignore());

            CreateMap<ProjectCreateDTO, Project>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src =>
                    src.ImageUrls.Select(url => new ImageUrl { Url = url }).ToList()
                )).ReverseMap();

            CreateMap<ProjectUpdateDTO, Project>()
                .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src =>
                    src.ImageUrls != null ? src.ImageUrls.Select(url => new ImageUrl { Url = url }).ToList() : new List<ImageUrl>()
                ))
                .ForMember(dest => dest.AgeRange, opt => opt.MapFrom(src =>
                    (AgeRange)src.AgeRange
                ));

            CreateMap<CommentCreateDTO, Comment>();

            CreateMap<CommentUpdateDTO, Comment>();

            CreateMap<Comment, CommentDTO>().ReverseMap();

            CreateMap<Comment, CommentResponseDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User != null ? src.User.Name : "Usuário Desconhecido"))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<School, SchoolDTO>().ReverseMap();
        }
    }
}