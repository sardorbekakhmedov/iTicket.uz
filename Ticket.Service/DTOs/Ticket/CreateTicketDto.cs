namespace Ticket.Service.DTOs.Ticket;

public class CreateTicketDto
{
    public required uint  EventId { get; set; }

    public required string Username { get; set; }
    public int RowNumber { get; set; }
    public int PlaceNumber { get; set; }
}