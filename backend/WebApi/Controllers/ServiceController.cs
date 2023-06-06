using Application.DTO.Service;
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

        [HttpGet("{id:int}")]
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
            if (service == null)
            {
                return NotFound();
            }
            return Ok(service);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
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
                _serviceService.AddService(newService);
                return Ok();
            }
            return BadRequest();
        }   
    }
}
