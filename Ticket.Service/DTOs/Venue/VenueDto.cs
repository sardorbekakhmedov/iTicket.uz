namespace Ticket.Service.DTOs.Venue;

public class VenueDto
{
    public uint Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public int Rows { get; set; }
    public int SeatsInRow { get; set; }
    public int Capacity { get; set; }
}