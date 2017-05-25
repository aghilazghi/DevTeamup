using AutoMapper;
using DevTeamup.Dtos;
using DevTeamup.Models;

namespace DevTeamup.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<ApplicationUser, ApplicationUserDto>();
            Mapper.CreateMap<Teamup, TeamupDto>();
            Mapper.CreateMap<Notification, NotificationDto>();
            Mapper.CreateMap<Discussion, DiscussionDto>();
            Mapper.CreateMap<Reply, ReplyDto>();
        }
    }
}