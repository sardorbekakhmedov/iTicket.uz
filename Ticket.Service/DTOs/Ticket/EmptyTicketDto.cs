namespace Ticket.Service.DTOs.Ticket;

public class EmptyTicketDto
{
    public string EventName { get; set; } = null!;
    public uint RowNumber { get; set; }
    public uint PlaceNumber { get; set; }
    public decimal TicketPrice { get; set; }
    public DateTime StarDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}