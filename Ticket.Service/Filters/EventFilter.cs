using Ticket.Service.PaginationModels;

namespace Ticket.Service.Filters;

public class EventFilter : PaginationParams
{
    public string? Name { get; set; }
    public DateTime? CreatedDate { get; set; }
}