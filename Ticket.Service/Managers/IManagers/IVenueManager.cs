using Ticket.Service.DTOs.Venue;
using Ticket.Service.Filters;

namespace Ticket.Service.Managers.IManagers;

public interface IVenueManager
{
    ValueTask<VenueDto> InsertAsync(CreateVenueDto dto);
    ValueTask<IEnumerable<VenueDto>> GetAllAsync(VenueFilter filter);
}