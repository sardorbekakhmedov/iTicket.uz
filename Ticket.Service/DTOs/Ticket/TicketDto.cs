namespace Ticket.Service.DTOs.Ticket;

public class TicketDto
{
    public uint Id { get; set; }
    public required string EventName { get; set; }

    public required string Username { get; set; }
    public int RowNumber { get; set; }
    public int PlaceNumber { get; set; }
    public required string SecretKey { get; set; }

    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}