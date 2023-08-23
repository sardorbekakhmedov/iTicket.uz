using Ticket.Domain.Entities;
using Ticket.Service.DTOs.Event;
using Ticket.Service.DTOs.Ticket;
using Ticket.Service.Filters;

namespace Ticket.Service.Managers.IManagers;

public interface IEventManager
{
    ValueTask<EventDto> InsertAsync(CreateEventDto dto);
    ValueTask<IEnumerable<EventDto>> GetAllAsync(EventFilter filter);
    ValueTask<EventDtoWithTickets> GetEventByIdAsync(uint eventId);
    ValueTask<IEnumerable<EmptyTicketDto>> GetAvailableTicketsAsync(uint eventId);
    ValueTask<Event?> GetEventByIdWithNavigationPropertyAsync(uint eventId);
}