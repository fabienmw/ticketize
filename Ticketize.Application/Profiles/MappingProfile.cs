using AutoMapper;
using Ticketize.Application.Features.Events;
using Ticketize.Domain.Entities;

namespace Ticketize.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventListVm>().ReverseMap();
        }
    }
}
