using Microsoft.AspNetCore.Mvc;
using StackOverflow.TagManagement.Api.DTO;
using StackOverflow.TagManagement.Api.Services;

namespace StackOverflow.TagManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> RefreshTagsAsync([FromServices]ITagsService tagsService,
        [FromQuery] uint startPage = 1,
        [FromQuery] uint pageCount = 20,
        CancellationToken cancellationToken = default)
    {
        await tagsService.GetTagsFromStackOverflowAsync(startPage, pageCount, cancellationToken);
        return Ok(new ResponseDto(Data: Constants.Messages.SuccessfullyRefreshedTags));
    }
}
