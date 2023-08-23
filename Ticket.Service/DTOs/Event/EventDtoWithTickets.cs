using Ticket.Service.DTOs.Ticket;

namespace Ticket.Service.DTOs.Event;

public class EventDtoWithTickets
{
    public required string Name { get; set; }
    public DateTime StarDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public decimal TicketPrice { get; set; }
    public IEnumerable<EmptyTicketDto>? EmptyTickets { get; set; }
}