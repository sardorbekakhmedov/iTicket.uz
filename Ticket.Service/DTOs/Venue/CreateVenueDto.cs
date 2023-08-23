namespace Ticket.Service.DTOs.Venue;

public class CreateVenueDto
{
    public required string Name { get; set; }
    public required string Address { get; set; }
    public int Rows { get; set; }
    public int SeatsInRow { get; set; }
}