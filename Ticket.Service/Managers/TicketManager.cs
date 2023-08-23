using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ticket.Data.Repositories.GenericRepository;
using Ticket.Service.DTOs.Ticket;
using Ticket.Service.Exceptions;
using Ticket.Service.Extensions;
using Ticket.Service.Filters;
using Ticket.Service.Managers.IManagers;
using Ticket.Service.PaginationModels;

namespace Ticket.Service.Managers;

public class TicketManager : ITicketManager
{
    private readonly IMapper _mapper;
    private readonly HttpContextHelper _httpContextHelper;
    private readonly IGenericRepository<Domain.Entities.Ticket> _ticketRepository;
    private readonly IEventManager _eventManager;

    public TicketManager(IMapper mapper, HttpContextHelper httpContextHelper,
        IGenericRepository<Domain.Entities.Ticket> ticketRepository,
        IEventManager eventManager)
    {
        _mapper = mapper;
        _httpContextHelper = httpContextHelper;
        _ticketRepository = ticketRepository;
        _eventManager = eventManager;
    }

    public async ValueTask<TicketDto> InsertAsync(CreateTicketDto dto)
    {
        var ev = await _eventManager.GetEventByIdWithNavigationPropertyAsync(dto.EventId);

        if (ev is null)
            throw new NotFoundException("Event not found!");

        var tickets = await _eventManager.GetAvailableTicketsAsync(ev.Id);

        var hasTicket = tickets.Any(s => s.RowNumber == dto.RowNumber && s.PlaceNumber == dto.PlaceNumber);

        if (!hasTicket)
            throw new NotFoundException("Ticket not found!");

        var ticket = _mapper.Map<Domain.Entities.Ticket>(dto);

        ticket.SecretKey = Guid.NewGuid().ToString();
        ticket.EventName = ev.Name;
        ticket.StartDateTime = ev.StarDateTime;
        ticket.EndDateTime = ev.EndDateTime;

        var newVenue = await _ticketRepository.InsertAsync(ticket);

        return _mapper.Map<TicketDto>(newVenue);
    }

    public async ValueTask<IEnumerable<TicketDto>> GetAllAsync(TicketFilter filter)
    {
        var query = _ticketRepository.SelectAll();

        if(filter.EventId is not null)
            query = query.Where(t => t.EventId.Equals(filter.EventId));

        if (filter.Username is not null)
            query = query.Where(t => t.Username.ToLower().Contains(filter.Username.ToLower()));

        if (filter.CreatedDate is not null)
            query = query.Where(t => t.CreatedAt >= filter.CreatedDate);


        var tickets = await query.AsNoTracking().ToPagedListAsync(_httpContextHelper, filter);

        return tickets.Select(v => _mapper.Map<TicketDto>(v));
    }

    public async ValueTask<TicketDto> GetTicketByIdAsync(uint ticketId)
    {
        var ticket = await _ticketRepository.SelectSingleAsync(t => t.Id.Equals(ticketId));

        if (ticket is null)
            throw new NotFoundException("Ticket not found!");

        return _mapper.Map<TicketDto>(ticket);
    }

    public async ValueTask CancelTicket(uint ticketId, string secretKey)
    {
        var ticket = await _ticketRepository.SelectSingleAsync(t => t.Id.Equals(ticketId));

        if (ticket is null)
            throw new NotFoundException("Ticket not found!");

        if(ticket.SecretKey != secretKey)
            throw new ArgumentException("Invalid secretKey");

        if ((ticket.StartDateTime - DateTime.UtcNow) > TimeSpan.FromDays(1))
            await _ticketRepository.DeleteAsync(ticket);
        else
            throw new TimeoutException("If less than ad day is left before the concert, the ticket is non-refundable!");
    }
}