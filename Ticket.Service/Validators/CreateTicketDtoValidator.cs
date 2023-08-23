using FluentValidation;
using Ticket.Service.DTOs.Ticket;

namespace Ticket.Service.Validators;

public class CreateTicketDtoValidator : AbstractValidator<CreateTicketDto>
{
    public CreateTicketDtoValidator()
    {
        RuleFor(t => t.EventId).NotNull().When(c => c.EventId > 0);
        RuleFor(t => t.Username).NotEmpty().MinimumLength(3);
        RuleFor(t => t.RowNumber).NotNull().When(c => c.RowNumber > 0);
        RuleFor(t => t.PlaceNumber).NotNull().When(c => c.PlaceNumber > 0);
    }
}