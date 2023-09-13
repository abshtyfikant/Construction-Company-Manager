using Application.DTO.ResourceAllocation;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ResourceAllocationController : ControllerBase
{
    private readonly IResourceAllocationService _resourceAllocationService;

    public ResourceAllocationController(IResourceAllocationService resourceAllocationService)
    {
        _resourceAllocationService = resourceAllocationService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ResourceAllocationDto>> Get()
    {
        var list = _resourceAllocationService.GetAllResourceAllocations();
        return Ok(list);
    }

    [HttpGet("{id:int}", Name = "GetResourceAllocation")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ResourceAllocationDto> Get(int id)
    {
        if (id <= 0) return BadRequest();
        var resourceAllocation = _resourceAllocationService.GetResourceAllocation(id);
        if (resourceAllocation is null) return NotFound();
        return Ok(resourceAllocation);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Create([FromBody] NewResourceAllocationDto newResourceAllocation)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (newResourceAllocation.EndDate > newResourceAllocation.BeginDate) return BadRequest();
        if (newResourceAllocation.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);
        var resourceAllocationId = _resourceAllocationService.AddResourceAllocation(newResourceAllocation);
        newResourceAllocation.Id = resourceAllocationId;
        return CreatedAtRoute("GetResourceAllocation", new { id = resourceAllocationId }, newResourceAllocation);
    }

    [HttpDelete("{id:int}", Name = "DeleteResourceAllocation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(int id)
    {
        if (id <= 0) return BadRequest();
        _resourceAllocationService.DeleteResourceAllocation(id);
        return NoContent();
    }

    [HttpPatch("{id:int}", Name = "UpdateResourceAllocation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update(int id, [FromBody] UpdateResourceAllocationDto resourceAllocation)
    {
        if (id <= 0) return BadRequest();
        if (resourceAllocation.EndDate > resourceAllocation.BeginDate) return BadRequest();
        if (!ModelState.IsValid) return BadRequest();
        _resourceAllocationService.UpdateResourceAllocation(resourceAllocation);
        return NoContent();
    }

    [HttpGet("Resource/{resourceId:int}", Name = "GetResourceAllocationsByResourceId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<ResourceAllocationDto>> GetResourceAllocationsByResourceId(int resourceId)
    {
        if (resourceId <= 0) return BadRequest();
        var resourceAllocations = _resourceAllocationService.GetResourceAllocationsByResourceId(resourceId);
        if (resourceAllocations is null) return NotFound();
        return Ok(resourceAllocations);
    }

    [HttpGet("Service/{serviceId:int}", Name = "GetResourceAllocationsByServiceId")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<ResourceAllocationDto>> GetResourceAllocationsByServiceId(int serviceId)
    {
        if (serviceId <= 0) return BadRequest();
        var resourceAllocations = _resourceAllocationService.GetResourceAllocationsByServiceId(serviceId);
        if (resourceAllocations is null) return NotFound();
        return Ok(resourceAllocations);
    }
}