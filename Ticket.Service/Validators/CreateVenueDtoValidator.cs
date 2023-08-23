using FluentValidation;
using Ticket.Service.DTOs.Venue;

namespace Ticket.Service.Validators;

public class CreateVenueDtoValidator : AbstractValidator<CreateVenueDto>
{
    public CreateVenueDtoValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(3);
        RuleFor(v => v.Rows).NotNull().When(c => c.Rows > 0);
        RuleFor(v => v.SeatsInRow).NotNull().When(c => c.SeatsInRow > 0);
    }
}