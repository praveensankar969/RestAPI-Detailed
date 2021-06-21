using System.Linq;
using API.DTO;
using AutoMapper;
using RestAPI_Detailed.DTO;

namespace RestAPI_Detailed.Core
{
    public class MappingProfiles : AutoMapper.Profile
    { 
        public MappingProfiles(){
            
            CreateMap<AppUser, RestAPI_Detailed.DTO.Profile>()
            .ForMember(d=> d.Image, o=> o.MapFrom(s=> s.Photos.FirstOrDefault(x=> x.IsMain==true).Url));
            
            CreateMap<Activity, Activity>();
           
            CreateMap<Activity, ActivityDTO>().ForMember(d=> d.HostName, o=> o.MapFrom(s=> s.Attendees.FirstOrDefault(x=> x.IsHost==true).AppUser.UserName));
            
            CreateMap<ActivityAttendee, AttendeeDTO>()
            .ForMember(d=> d.DisplayName, o=> o.MapFrom(s=>s.AppUser.DisplayName))
            .ForMember(d=> d.Bio , o=> o.MapFrom(s=>s.AppUser.Bio))
            .ForMember(d=> d.UserName , o=> o.MapFrom(s=>s.AppUser.UserName))
            .ForMember(d=> d.Image , o=> o.MapFrom(s=>s.AppUser.Photos.FirstOrDefault(x=> x.IsMain==true).Url));

            CreateMap<Comment, CommentDTO>()
            .ForMember(d => d.UserName, o => o.MapFrom(x => x.Author.UserName))
            .ForMember(d => d.DisplayName, o => o.MapFrom(x => x.Author.DisplayName))
            .ForMember(d => d.Image, o => o.MapFrom(x => x.Author.Photos.FirstOrDefault(s => s.IsMain==true).Url));
        }
    }
}