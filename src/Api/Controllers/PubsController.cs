using Api.Filters;
using Core.Interfaces;
using Core.Models;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Produces(Constants.JsonContentType)]
[Route("api/[controller]")]
public class PubsController : ControllerBase
{
    private readonly IPubsSearchService _service;
    private readonly ILogger<PubsController> _logger;

    public PubsController(IPubsSearchService service, ILogger<PubsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    [ValidateModel]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Pubs>))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<Pubs>>> Search([FromBody] PubsSearchParameters searchCriteria)
    {
        var result = await _service.SearchPubs(searchCriteria);

        var pubsFound = result.ToList();
        if (!pubsFound.Any())
        {
            _logger.LogInformation("No content available with {searchCriteria}", searchCriteria);
            return NoContent();
        }

        _logger.LogInformation("{Rows} records found with {searchCriteria}", pubsFound.Count(), searchCriteria);
        return Ok(result);
    }
}