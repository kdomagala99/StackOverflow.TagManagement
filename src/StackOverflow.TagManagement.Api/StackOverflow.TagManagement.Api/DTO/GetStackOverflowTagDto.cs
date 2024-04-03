namespace StackOverflow.TagManagement.Api.DTO
{
    public record GetStackOverflowTagDto : StackOverflowTagDto
    {
        public double TagPercentage { get; init; }

        public GetStackOverflowTagDto(double tagPercentage, StackOverflowTagDto other)
            : base(other)
        {
            this.TagPercentage = tagPercentage;
        }
    }
}
