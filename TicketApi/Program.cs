using Serilog;
using Serilog.Events;
using TicketApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration().WriteTo.File(@"Loggers\Errors.txt", LogEventLevel.Error,
    rollingInterval: RollingInterval.Day).CreateLogger();

builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRepositoriesAndManagers();
builder.Services.AddCustomServices();
builder.Services.AddDbContextWithConnections(builder.Configuration);
builder.Services.AddFluentValidators();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
