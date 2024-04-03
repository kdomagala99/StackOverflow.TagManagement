using Microsoft.AspNetCore.Mvc;
using StackOverflow.TagManagement.Api.DTO;
using StackOverflow.TagManagement.Api.Services;

namespace StackOverflow.TagManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTagsAsync([FromServices]ITagsService tagsService,
        [FromQuery] int startPage = 1,
        [FromQuery] int pageCount = 20,
        CancellationToken cancellationToken = default)
    {
        await tagsService.GetTagsFromStackOverflowAsync(startPage, pageCount, cancellationToken);
        return Ok(new ResponseDto(Data: Constants.Messages.SuccessfullyRefreshedTags));
    }

    [HttpGet]
    public async Task<IActionResult> GetTagsAsync([FromServices]ITagsService tagsService,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 20,
        [FromQuery] Order orderByName = Order.None,
        [FromQuery] Order orderByTagPercentage = Order.None,
        CancellationToken cancellationToken = default)
        => Ok(new ResponseDto(Data: await tagsService.GetTagsFromLocalDbAsync(skip, take, orderByName, orderByTagPercentage, cancellationToken)));
}
