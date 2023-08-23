using FluentValidation;
using Ticket.Data.Repositories.GenericRepository;
using Ticket.Service.DTOs.Event;
using Ticket.Service.DTOs.Ticket;
using Ticket.Service.DTOs.Venue;
using Ticket.Service.Managers;
using Ticket.Service.Managers.IManagers;
using Ticket.Service.Mappers;
using Ticket.Service.PaginationModels;
using Ticket.Service.Validators;

namespace TicketApi.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static void AddRepositoriesAndManagers(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IVenueManager, VenueManager>();
        services.AddScoped<IEventManager, EventManager>();
        services.AddScoped<ITicketManager, TicketManager>();
    }

    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<HttpContextHelper>();
        services.AddAutoMapper(typeof(MapperProfile));
    }

    public static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateVenueDto>, CreateVenueDtoValidator>();
        services.AddScoped<IValidator<CreateEventDto>, CreateEventDtoValidator>();
        services.AddScoped<IValidator<CreateTicketDto>, CreateTicketDtoValidator>();
    }
}