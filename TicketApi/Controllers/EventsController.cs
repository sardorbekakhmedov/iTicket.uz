using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Ticket.Service.DTOs.Event;
using Ticket.Service.Exceptions;
using Ticket.Service.Filters;
using Ticket.Service.Managers.IManagers;

namespace TicketApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IEventManager _eventManager;

    public EventsController(IEventManager eventManager)
    {
        _eventManager = eventManager;
    }

    [HttpPost]
    public async ValueTask<IActionResult> Insert(CreateEventDto dto, [FromServices] IValidator<CreateEventDto> validator)
    {
        var result = await validator.ValidateAsync(dto);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        try
        {
            return Created("Insert", await _eventManager.InsertAsync(dto));
        }
        catch (VenueBusyException e)
        {
            return BadRequest(e.Message);
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetAll([FromQuery] EventFilter filter)
    {
        if (!ModelState.IsValid)
            return BadRequest(filter);

        try
        {
            return Ok(await _eventManager.GetAllAsync(filter));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("{eventId}")]
    public async ValueTask<IActionResult> GetEventWithEmptyTickets(uint eventId)
    {
        try
        {
            return Ok(await _eventManager.GetEventByIdAsync(eventId));
        }
        catch (NotFoundException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}