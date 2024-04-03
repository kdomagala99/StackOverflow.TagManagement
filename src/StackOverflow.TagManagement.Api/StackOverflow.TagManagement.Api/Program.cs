using StackOverflow.TagManagement.Api.Extensions;
using StackOverflow.TagManagement.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithConfiguration();
services.AddOpenApiData();
services.AddHttpClientWithHandler();
services.AddSingletons();
services.AddScopeds();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger?.LogTrace("Trace first log message from application:{CurrentUtcTime}", DateTime.UtcNow);
}

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    await app.Services.GetFirstTagsAsync();
}

app.UseMiddleware<LoggingMiddleware>();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
