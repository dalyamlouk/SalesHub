using SalesHub.Api.Filters;
using SalesHub.Api.Middleware;
using SalesHub.Application;
using SalesHub.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure();
builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
