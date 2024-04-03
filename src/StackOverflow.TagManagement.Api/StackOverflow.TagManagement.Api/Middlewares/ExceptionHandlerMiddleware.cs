using StackOverflow.TagManagement.Api.DTO;
using StackOverflow.TagManagement.Api.Exceptions;
using StackOverflow.TagManagement.Api.Extensions;
using System.Net;
using System.Text.Json;

namespace StackOverflow.TagManagement.Api.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionHandlerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

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
            await PrepareResponse(response, exception.Message);
        }
        catch
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await PrepareResponse(response, Constants.Messages.InternalServerErrorException);
        }
    }

    private static async Task PrepareResponse(HttpResponse response, string message)
    {
        var result = new ResponseDto(Error: message);
        await response.WriteAsync(JsonSerializer.Serialize(result, JsonSerializerOptionsExtension.WriteOptions));
    }
}
