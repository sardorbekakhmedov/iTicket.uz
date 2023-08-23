using Microsoft.EntityFrameworkCore;
using Ticket.Data.Context;

namespace TicketApi.Extensions;

public partial class ServiceCollectionExtensions
{
    public static void AddDbContextWithConnections(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            options.UseSnakeCaseNamingConvention()
                .UseInMemoryDatabase("TicketDb");
            //.UseNpgsql(configuration.GetConnectionString("TicketDb"));
        });
    }
}