using AutoMapper;
using BetsService.Domain;
using BetsService.Models;

namespace BetsService.Services.Helpers
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EventRequest, Events>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(d => d.EventStartTime, opt => opt.MapFrom(src => src.EventStartTime.ToUniversalTime()))
                .ForMember(d => d.BetsEndTime, opt => opt.MapFrom(src => src.BetsEndTime.ToUniversalTime()));
            CreateMap<Events, EventResponse>();

            CreateMap<EventOutcomeRequest, EventOutcomes>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<EventOutcomes, EventOutcomeResponse>();

            CreateMap<BetsRequest, Domain.Bets>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<Domain.Bets, BetsResponse>();
        }
    }
}
