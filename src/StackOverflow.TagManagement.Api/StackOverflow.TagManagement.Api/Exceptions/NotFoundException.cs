using System.Net;

namespace StackOverflow.TagManagement.Api.Exceptions
{
    public class NotFoundException : GenericException
    {
        public override int HttpCode { get; } = (int)HttpStatusCode.NotFound;

        public NotFoundException() 
            : base(Constants.Messages.NotFoundException)
        {
        }
    }
}
