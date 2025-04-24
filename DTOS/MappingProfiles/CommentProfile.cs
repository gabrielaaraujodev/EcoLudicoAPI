using AutoMapper;
using EcoLudicoAPI.DTOS;
using EcoLudicoAPI.Models;

namespace EcoLudicoAPI.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDTO>().ReverseMap();
        }
    }
}
