using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ticket.Data.Repositories.GenericRepository;
using Ticket.Domain.Entities;
using Ticket.Service.DTOs.Event;
using Ticket.Service.DTOs.Ticket;
using Ticket.Service.Exceptions;
using Ticket.Service.Extensions;
using Ticket.Service.Filters;
using Ticket.Service.Managers.IManagers;
using Ticket.Service.PaginationModels;

namespace Ticket.Service.Managers;

public class EventManager : IEventManager
{
    private readonly IMapper _mapper;
    private readonly HttpContextHelper _httpContextHelper;
    private readonly IGenericRepository<Event> _eventRepository;

    public EventManager(IMapper mapper, HttpContextHelper httpContextHelper,
        IGenericRepository<Event> eventRepository)
    {
        _mapper = mapper;
        _httpContextHelper = httpContextHelper;
        _eventRepository = eventRepository;
    }
    
    public async ValueTask<EventDto> InsertAsync(CreateEventDto dto)
    {
        var isExistingEvent = await _eventRepository
            .HasAnyAsync(e => e.EndDateTime >= dto.StarDateTime && e.StarDateTime <= dto.EndDateTime);
        
        if (isExistingEvent) 
            throw new VenueBusyException("Venue is busy");

        var createEvent = _mapper.Map<Event>(dto);

        var newEvent = await _eventRepository.InsertAsync(createEvent);

        return _mapper.Map<EventDto>(newEvent);
    }

    public async ValueTask<EventDtoWithTickets> GetEventByIdAsync(uint eventId)
    {
        var ev = await GetEventByIdWithNavigationPropertyAsync(eventId);

        if (ev is null)
            throw new NotFoundException($"{nameof(Event)} not found!");

        var tickets = await GetAvailableTicketsAsync(ev.Id);

        return ev.ToEventDtoWithEmptyTickets(tickets);
    }

    public async ValueTask<IEnumerable<EmptyTicketDto>> GetAvailableTicketsAsync(uint eventId)
    {
        var ev = await GetEventByIdWithNavigationPropertyAsync(eventId);

        if(ev is null)
            throw new NotFoundException($"{nameof(Event)} not found!");

        return GetEmptyTickets(ev);
    }

    public async ValueTask<IEnumerable<EventDto>> GetAllAsync(EventFilter filter)
    {
        var query = _eventRepository.SelectAll();

        if (filter.Name is not null)
            query = query.Where(v => v.Name.ToLower().Contains(filter.Name.ToLower()));

        if (filter.CreatedDate is not null)
            query = query.Where(v => v.CreatedAt == filter.CreatedDate);

        var events = await query.AsNoTracking().ToPagedListAsync(_httpContextHelper, filter);

        return events.Select(ev => _mapper.Map<EventDto>(ev));
    }

    public async ValueTask<Event?> GetEventByIdWithNavigationPropertyAsync(uint eventId)
    {
        return await _eventRepository.SelectAll()
            .Include(e => e.Venue)
            .Include(e => e.Tickets)
            .SingleOrDefaultAsync(e => e.Id.Equals(eventId));
    }

    private IEnumerable<EmptyTicketDto> GetEmptyTickets(Event ev)
    {
        var emptyTickets = new List<EmptyTicketDto>();
        uint placeNumber = 1;

        for (uint i = 0; i < ev.Venue.Rows; i++)
        {
            for (uint j = 0; j < ev.Venue.SeatsInRow; j++)
            {
                bool hasTicket = false;

                if (ev.Tickets is not null && ev.Tickets.Count > 0)
                    hasTicket = ev.Tickets.Any(s => s.RowNumber == i + 1 && s.PlaceNumber == placeNumber);

                if (hasTicket)
                {
                    placeNumber++;
                    continue;
                }

                var emptyTicket = new EmptyTicketDto()
                {
                    EventName = ev.Name,
                    RowNumber = i + 1,
                    PlaceNumber = placeNumber++,
                    TicketPrice = ev.TicketPrice,
                    StarDateTime = ev.StarDateTime,
                    EndDateTime = ev.EndDateTime
                };

                emptyTickets.Add(emptyTicket);
            }
        }
        return emptyTickets;
    }
}