using Ticket.Domain.Entities;
using Ticket.Service.DTOs.Event;
using Ticket.Service.DTOs.Ticket;

namespace Ticket.Service.Extensions;

public static class EventExtensions 
{
    public static EventDtoWithTickets ToEventDtoWithEmptyTickets(this Event ev, IEnumerable<EmptyTicketDto> emptyTickets)
    {
        return new EventDtoWithTickets()
        {
            Name = ev.Name,
            StarDateTime = ev.StarDateTime,
            EndDateTime = ev.EndDateTime,
            TicketPrice = ev.TicketPrice,
            EmptyTickets = emptyTickets
        };
    }
}