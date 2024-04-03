using System.Net;

namespace StackOverflow.TagManagement.Api.Exceptions
{
    public class InsufficientStorageException : GenericException
    {
        public override int HttpCode { get; } = (int)HttpStatusCode.InsufficientStorage;

        public InsufficientStorageException()
            : base(Constants.Messages.InsufficientStorageException)
        {
        }
    }
}
