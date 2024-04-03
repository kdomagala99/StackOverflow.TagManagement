using Microsoft.AspNetCore.Mvc;
using StackOverflow.TagManagement.Api.DTO;
using StackOverflow.TagManagement.Api.Examples;
using StackOverflow.TagManagement.Api.Services;
using Swashbuckle.AspNetCore.Filters;

namespace StackOverflow.TagManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TagsController : ControllerBase
{
    /// <summary>
    /// Refreshes list of tag by fetching it from stackoverflow.
    /// </summary>
    /// <param name="tagsService">Service for executing request.</param>
    /// <param name="startPage">Starting page where from the fetching will start.</param>
    /// <param name="pageCount">Number of pages which should be fetched.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Message with status.</returns>
    /// <response code=200>Tags refreshed successfully</response>
    /// <response code=500>Service error. Service has encountered one or more unexpected conditions that prevented it from fulfilling the request, which may or may not be temporary.</response>
    /// <response code=503>Service unavailable. Service has not been able to create connection to StackOverflow API.</response>
    /// <response code=507>Insufficient storage. Insufficient storage or other database related issue prevented from fulfilling the request.</response>
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(ResponseDto), 200)]
    [SwaggerResponseExample(200, typeof(TagServiceRefreshExample))]
    [ProducesResponseType(500)]
    [ProducesResponseType(503)]
    [ProducesResponseType(507)]
    public async Task<IActionResult> RefreshTagsAsync([FromServices]ITagsService tagsService,
        [FromQuery] int startPage = 1,
        [FromQuery] int pageCount = 20,
        CancellationToken cancellationToken = default)
    {
        await tagsService.GetTagsFromStackOverflowAsync(startPage, pageCount, cancellationToken);
        return Ok(new ResponseDto(Data: Constants.Messages.SuccessfullyRefreshedTags));
    }

    /// <summary>
    /// Gets list of tags from local database.
    /// </summary>
    /// <param name="tagsService">Service for executing request</param>
    /// <param name="skip">Number of rows to skip.</param>
    /// <param name="take">Number of rows to take.</param>
    /// <param name="orderByName">Order by name</param>
    /// <param name="orderByTagPercentage">Order by tag percentage.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of tags.</returns>
    /// <response code=200>List of tags from local database.</response>
    /// <response code=404>Not found. Service established connection but somehow it wasn't able to find requested data.</response>
    /// <response code=500>Service error. Service has encountered one or more unexpected conditions that prevented it from fulfilling the request, which may or may not be temporary</response>
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDto), 200)]
    [SwaggerResponseExample(200, typeof(TagsServiceListExample))]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetTagsAsync([FromServices]ITagsService tagsService,
        [FromQuery] int skip = 0,
        [FromQuery] int take = 20,
        [FromQuery] Order orderByName = Order.None,
        [FromQuery] Order orderByTagPercentage = Order.None,
        CancellationToken cancellationToken = default)
        => Ok(new ResponseDto(Data: await tagsService.GetTagsFromLocalDbAsync(skip, take, orderByName, orderByTagPercentage, cancellationToken)));
}
