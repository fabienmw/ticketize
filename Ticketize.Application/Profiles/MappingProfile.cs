using AutoMapper;
using Ticketize.Application.Features.Events;
using Ticketize.Application.Features.Events.Queries;
using Ticketize.Domain.Entities;

namespace Ticketize.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventListVm>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Category, CategoryDto>();
        }
    }
}
