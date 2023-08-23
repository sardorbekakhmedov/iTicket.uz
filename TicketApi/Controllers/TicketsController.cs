using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Ticket.Service.DTOs.Ticket;
using Ticket.Service.Exceptions;
using Ticket.Service.Filters;
using Ticket.Service.Managers.IManagers;

namespace TicketApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketManager _ticketManager;

    public TicketsController(ITicketManager ticketManager)
    {
        _ticketManager = ticketManager;
    }

    [HttpPost]
    public async ValueTask<IActionResult> Insert(CreateTicketDto dto, [FromServices] IValidator<CreateTicketDto> validator)
    {
        var result = await validator.ValidateAsync(dto);

        if (!result.IsValid)
            return BadRequest(result.Errors);

        try
        {
            return Created("Insert", await _ticketManager.InsertAsync(dto));
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
    public async ValueTask<IActionResult> GetAll([FromQuery] TicketFilter filter)
    {
        if (!ModelState.IsValid)
            return BadRequest(filter);

        try
        {
            return Ok(await _ticketManager.GetAllAsync(filter));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("{ticketId}")]
    public async ValueTask<IActionResult> GetTicketById(uint ticketId)
    {
        try
        {
            return Ok(await _ticketManager.GetTicketByIdAsync(ticketId));
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

    [HttpDelete]
    public async ValueTask<IActionResult> DeleteEventTickets(uint ticketId, string secretKey)
    {
        try
        {
            await _ticketManager.CancelTicket(ticketId, secretKey);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (TimeoutException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}