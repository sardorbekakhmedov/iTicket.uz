using AutoMapper;
using Ticket.Domain.Entities;
using Ticket.Service.DTOs.Event;
using Ticket.Service.DTOs.Ticket;
using Ticket.Service.DTOs.Venue;

namespace Ticket.Service.Mappers;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Venue, CreateVenueDto>().ReverseMap();
        CreateMap<VenueDto, Venue>().ReverseMap();

        CreateMap<Event, CreateEventDto>().ReverseMap();
        CreateMap<EventDto, Event>().ReverseMap();

        CreateMap<Domain.Entities.Ticket, CreateTicketDto>().ReverseMap();
        CreateMap<TicketDto, Domain.Entities.Ticket>().ReverseMap();
    }
}