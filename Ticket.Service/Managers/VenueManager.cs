using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ticket.Data.Repositories.GenericRepository;
using Ticket.Domain.Entities;
using Ticket.Service.DTOs.Venue;
using Ticket.Service.Extensions;
using Ticket.Service.Filters;
using Ticket.Service.Managers.IManagers;
using Ticket.Service.PaginationModels;

namespace Ticket.Service.Managers;

public class VenueManager : IVenueManager
{
    private readonly IMapper _mapper;
    private readonly HttpContextHelper _httpContextHelper;
    private readonly IGenericRepository<Venue> _venueRepository;

    public VenueManager(IMapper mapper, HttpContextHelper httpContextHelper ,IGenericRepository<Venue> venueRepository)
    {
        _mapper = mapper;
        _httpContextHelper = httpContextHelper;
        _venueRepository = venueRepository;
    }

    public async ValueTask<VenueDto> InsertAsync(CreateVenueDto dto)
    {
        var venue = _mapper.Map<Venue>(dto);

       var newVenue = await _venueRepository.InsertAsync(venue);

       return _mapper.Map<VenueDto>(newVenue);
    }

    public async ValueTask<IEnumerable<VenueDto>> GetAllAsync(VenueFilter filter)
    {
        var query = _venueRepository.SelectAll();

        if (filter.Name is not null)
            query = query.Where(v => v.Name.ToLower().Contains(filter.Name.ToLower()));

        if (filter.CreatedDate is not null)
            query = query.Where(v => v.CreatedAt == filter.CreatedDate);


        var venues = await query.AsNoTracking().ToPagedListAsync(_httpContextHelper, filter);

        return venues.Select(v => _mapper.Map<VenueDto>(v));
    }
}