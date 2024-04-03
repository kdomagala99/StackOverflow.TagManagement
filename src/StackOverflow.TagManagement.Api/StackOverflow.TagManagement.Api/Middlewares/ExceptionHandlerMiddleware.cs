using StackOverflow.TagManagement.Api.DTO;
using StackOverflow.TagManagement.Api.Exceptions;
using StackOverflow.TagManagement.Api.Extensions;
using System.Net;
using System.Text.Json;

namespace StackOverflow.TagManagement.Api.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
{
    private readonly RequestDelegate next = next;
    private readonly ILogger logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch (GenericException exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = exception.HttpCode;
            await PrepareResponse(context, exception, response, exception.Message);
        }
        catch (Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await PrepareResponse(context, exception, response, Constants.Messages.InternalServerErrorException);
        }
    }

    private async Task PrepareResponse(HttpContext httpContext, Exception exception, HttpResponse response, string message)
    {
        this.logger.LogError("{UtcTime}: Error for request {RequestUri} is {Message}", DateTime.UtcNow, httpContext.Request.Path, exception);
        var result = new ResponseDto(Error: message);
        await response.WriteAsync(JsonSerializer.Serialize(result, JsonSerializerOptionsExtension.WriteOptions));
    }
}
