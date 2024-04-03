namespace StackOverflow.TagManagement.Api.Exceptions;

public abstract class GenericException : Exception
{
    public abstract int HttpCode { get; }

    public GenericException(string? message = null)
        : base(message)
    {
    }
}
