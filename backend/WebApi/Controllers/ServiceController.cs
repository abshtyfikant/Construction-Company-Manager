using Application.DTO.Service;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ServiceController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServiceController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ServiceForListDto>> Get()
    {
        var list = _serviceService.GetServicesForList();
        return Ok(list);
    }

    [HttpGet("{id:int}", Name = "GetService")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ServiceDetailsDto> Get(int id)
    {
        if (id == 0) return BadRequest();
        var service = _serviceService.GetService(id);
        if (service is null) return NotFound();
        return Ok(service);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Create([FromBody] AllServiceDto newService)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (newService.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);

        var onlyService = new NewServiceDto
        {
            Id = newService.Id,
            ClientId = newService.ClientId,
            ServiceType = newService.ServiceType,
            BeginDate = newService.BeginDate,
            EndDate = newService.EndDate,
            ServiceStatus = newService.ServiceStatus,
            PaymentStatus = newService.PaymentStatus,
            City = newService.City,
            Price = newService.Price,
        };

        var serviceId = _serviceService.AddService(onlyService, newService.Assigments, newService.Resources, newService.Materials);
        newService.Id = serviceId;
        return CreatedAtRoute("GetService", new { id = serviceId }, newService);
    }

    [HttpDelete("{id:int}", Name = "DeleteService")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(int id)
    {
        if (id == 0) return BadRequest();
        _serviceService.DeleteService(id);
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromBody] AllServiceDto newService)
    {
        if (!this.ModelState.IsValid || newService.Id <= 0) return BadRequest();
        var onlyService = new NewServiceDto
        {
            Id = newService.Id,
            ClientId = newService.ClientId,
            ServiceType = newService.ServiceType,
            BeginDate = newService.BeginDate,
            EndDate = newService.EndDate,
            ServiceStatus = newService.ServiceStatus,
            PaymentStatus = newService.PaymentStatus,
            City = newService.City,
            Price = newService.Price,
        };

        _serviceService.UpdateService(onlyService, newService.Assigments, newService.Resources, newService.Materials);
        return NoContent();
    }

    [HttpGet("GetIncome/{begin:datetime}/{end:datetime}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Double> GetIncome(DateTime begin, DateTime end)
    {
        if (begin == null || end == null || begin > end) return BadRequest();
        var income = _serviceService.GetServiceEarnings(begin, end);
        return Ok(income);
    }

    [HttpGet("GetCost/{serviceId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<Double> GetCost(int serviceId)
    {
        var cost = _serviceService.GetServiceCost(serviceId);
        return Ok(cost);
    }
}
