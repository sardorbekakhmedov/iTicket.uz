using Ticket.Service.DTOs.Ticket;
using Ticket.Service.Filters;

namespace Ticket.Service.Managers.IManagers;

public interface ITicketManager
{
    ValueTask<TicketDto> InsertAsync(CreateTicketDto dto);
    ValueTask<IEnumerable<TicketDto>> GetAllAsync(TicketFilter filter);
    ValueTask<TicketDto> GetTicketByIdAsync(uint ticketId);
    ValueTask CancelTicket(uint ticketId, string secretKey);
}