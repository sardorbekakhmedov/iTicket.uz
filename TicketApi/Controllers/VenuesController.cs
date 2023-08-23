using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Ticket.Service.DTOs.Venue;
using Ticket.Service.Filters;
using Ticket.Service.Managers.IManagers;

namespace TicketApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VenuesController : ControllerBase
{
    private readonly IVenueManager _venueManager;

    public VenuesController(IVenueManager venueManager)
    {
        _venueManager = venueManager;
    }

    [HttpPost]
    public async ValueTask<IActionResult> Insert(CreateVenueDto dto, [FromServices] IValidator<CreateVenueDto> validator)
    {
        var result = await validator.ValidateAsync(dto);

        if(!result.IsValid)
            return BadRequest(result.Errors);

        try
        {
            return Created("Insert", await _venueManager.InsertAsync(dto));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }


    [HttpGet]
    public async ValueTask<IActionResult> GetAll([FromQuery] VenueFilter filter)
    {
        if (!ModelState.IsValid)
            return BadRequest(filter);

        try
        {
            return Ok(await _venueManager.GetAllAsync(filter));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}