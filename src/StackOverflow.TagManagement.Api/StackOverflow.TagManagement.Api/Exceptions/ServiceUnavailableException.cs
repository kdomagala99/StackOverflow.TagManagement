using System.Net;

namespace StackOverflow.TagManagement.Api.Exceptions;

public class ServiceUnavailableException : GenericException
{
    public override int HttpCode { get; } = (int)HttpStatusCode.ServiceUnavailable;

    public ServiceUnavailableException()
        : base (Constants.Messages.ServiceUnavailableException)
    {
        
    }
}
