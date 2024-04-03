using StackOverflow.TagManagement.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllersWithConfiguration();
services.AddOpenApiData();
services.AddHttpClientWithHandler();
services.AddSingletons(builder);
services.AddScopeds();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    await app.Services.GetFirstTags();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();