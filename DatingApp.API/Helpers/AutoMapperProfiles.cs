using System.Linq;
using AutoMapper;
using DatingApp.API.Dtos;
using DatingApp.API.Models;

namespace DatingApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,UserForListDto>().
            ForMember(dest=>dest.PhotoUrl,opt => {
                opt.MapFrom(src=> src.Photos.FirstOrDefault(x=>x.IsMain).Url);
            }).ForMember(dest=> dest.Age, opt=>{
                opt.ResolveUsing(d=>d.DateOfBirth.ClauclateAge());
            });
            
            CreateMap<User,UserForDetailedDto>().
            ForMember(dest=> dest.PhotoUrl, opt=> {
                opt.MapFrom(src=>src.Photos.FirstOrDefault(x=>x.IsMain).Url);
            }).ForMember(dest=>dest.Age,opt=>{
                opt.ResolveUsing(d=>d.DateOfBirth.ClauclateAge());
            });
            CreateMap<Photo,PhotosForDetailedDto>();
        }
    }
}