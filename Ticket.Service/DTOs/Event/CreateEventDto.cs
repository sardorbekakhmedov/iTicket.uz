namespace Ticket.Service.DTOs.Event;

public class CreateEventDto
{
    public uint VenueId { get; set; }

    public required string Name { get; set; }
    public DateTime StarDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public decimal TicketPrice { get; set; }
}