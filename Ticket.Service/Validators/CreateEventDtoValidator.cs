using FluentValidation;
using Ticket.Service.DTOs.Event;

namespace Ticket.Service.Validators;

public class CreateEventDtoValidator : AbstractValidator<CreateEventDto>
{
    public CreateEventDtoValidator()
    {
        RuleFor(e => e.Name).NotNull().MinimumLength(3);
        RuleFor(e => e.VenueId).NotNull().When(c => c.VenueId > 0);
        RuleFor(e => e.TicketPrice).NotNull().When(c => c.TicketPrice > 0);
    }
}