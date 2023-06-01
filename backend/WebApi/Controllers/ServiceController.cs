using Application.DTO.Service;
using Application.Interfaces;
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
        public ActionResult<IEnumerable<ServiceForListDto>> Get()
        {
            var model = _serviceService.GetServicesForList();
            if(model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpGet("{id:int}")]
        public string Get(int id)
        {
            return "value";
        }

    }
}
