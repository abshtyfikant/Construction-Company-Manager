using Application.DTO.Resource;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ResourceController : ControllerBase
{
    private readonly IResourceService _resourceService;

    public ResourceController(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ResourceDto>> Get()
    {
        var list = _resourceService.GetResourcesForList();
        return Ok(list);
    }

    [HttpGet("{id:int}", Name = "GetResource")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ResourceDto> Get(int id)
    {
        if (id == 0) return BadRequest();
        var resource = _resourceService.GetResource(id);
        if (resource is null) return NotFound();
        return Ok(resource);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Create([FromBody] NewResourceDto newResource)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (newResource.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);
        var resourceId = _resourceService.AddResource(newResource);
        newResource.Id = resourceId;
        return CreatedAtRoute("GetResource", new { id = resourceId }, newResource);
    }

    [HttpDelete("{id:int}", Name = "DeleteResource")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(int id)
    {
        if (id == 0) return BadRequest();
        _resourceService.DeleteResource(id);
        return NoContent();
    }

    [HttpPut(Name = "UpdateResource")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update(NewResourceDto resource)
    {
        if (resource.Id <= 0 || !ModelState.IsValid) return BadRequest();
        _resourceService.UpdateResource(resource);
        return NoContent();
    }

    [HttpPatch("{id:int}/{quantity:int}", Name = "ChangeQuantity")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult ChangeQuantity(int id, int quantity)
    {
        if (id <= 0 || quantity <= 0) return BadRequest();
        _resourceService.ChangeQuantity(id, quantity);
        return NoContent();
    }

    [HttpGet("Available/{id:int}/{startTime:datetime}/{endTime:datetime}", Name = "GetAvailableQuantityForTime")]
    public IActionResult GetAvailableQuantityForTime(int id, DateTime startTime, DateTime endTime)
    {
        if (id <= 0 || startTime >= endTime) return BadRequest();
        var quantity = _resourceService.GetAvailableQuantityForTime(id, startTime, endTime);
        return Ok(quantity);
    }
}