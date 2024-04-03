﻿using Microsoft.Extensions.Http;
using StackOverflow.TagManagement.Api.Configurations;
using StackOverflow.TagManagement.Api.Database;
using StackOverflow.TagManagement.Api.Services;
using System.Net;

namespace StackOverflow.TagManagement.Api.Extensions;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddHttpClientWithHandler(this IServiceCollection services)
    {
        services.ConfigureAll<HttpClientFactoryOptions>(options =>
        {
            options.HttpMessageHandlerBuilderActions.Add(builder =>
            {
                builder.PrimaryHandler = new HttpClientHandler
                {
                    AutomaticDecompression = DecompressionMethods.All
                };
            });
        });
        services.AddHttpClient();
        return services;
    }

    public static IServiceCollection AddOpenApiData(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    public static IServiceCollection AddSingletons(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddSingleton<IDbContext, MongoDbContext>(provider => new MongoDbContext(
            builder.Configuration.GetValue<MongoDbConfiguration>("MongoDbConfiguration") ?? new MongoDbConfiguration()));
        return services;
    }

    public static IServiceCollection AddScopeds(this IServiceCollection services)
    {
        services.AddScoped<ITagsService, TagsService>();
        return services;
    }
    
    public static IServiceCollection AddControllersWithConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddRouting(options => options.LowercaseUrls = true);
        return services;
    }
}