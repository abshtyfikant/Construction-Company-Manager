using Application.DTO.Report;
using Application.DTO.Service;
using Application.Interfaces.Reports;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
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
            if (id == 0)
            {
                return BadRequest();
            }
            var service = _serviceService.GetService(id);
            if (service is null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public  ActionResult Create ([FromBody]NewServiceDto newService)
        {
            if (ModelState.IsValid)
            { 
                if (newService.Id > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                var serviceId = _serviceService.AddService(newService);
                newService.Id = serviceId;
                return CreatedAtRoute("GetService", new {id = serviceId }, newService);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}", Name = "DeleteService")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            _serviceService.DeleteService(id);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Update([FromBody] NewServiceDto newService)
        {
            if (ModelState.IsValid)
            {
                if (newService.Id > 0)
                {
                    _serviceService.UpdateService(newService);
                    return NoContent();
                }
            }
            return BadRequest();
        }
    }
}
