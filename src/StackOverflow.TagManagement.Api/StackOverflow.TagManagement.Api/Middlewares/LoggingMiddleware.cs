namespace StackOverflow.TagManagement.Api.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger logger;

    public LoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        this.next = next;
        this.logger = loggerFactory.CreateLogger<LoggingMiddleware>();
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (IsNotPreflightRequest(context.Request.Method))
        {
            this.logger.LogDebug("Request: {method} {path}", context.Request.Method, context.Request.Path);
        }

        await this.next(context);
    }

    private static bool IsNotPreflightRequest(string method) => method != HttpMethod.Options.Method;
}
