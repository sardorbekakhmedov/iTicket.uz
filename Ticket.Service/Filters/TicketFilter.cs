using Ticket.Service.PaginationModels;

namespace Ticket.Service.Filters;

public class TicketFilter : PaginationParams
{
    public uint? EventId { get; set; }
    public string? Username { get; set; }
    public DateTime? CreatedDate { get; set; }
}