namespace Ticket.Service.DTOs.Event;

public class EventDto
{
    public uint Id { get; set; }
    public required string Name { get; set; }
    public DateTime StarDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public decimal TicketPrice { get; set; }
}