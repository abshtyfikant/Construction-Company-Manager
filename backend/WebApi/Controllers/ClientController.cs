using Application.DTO.Client;
using Application.DTO.Service;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ClientDto>> Get()
        {
            var list = _clientService.GetClientsForList();
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ClientDto> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var service = _clientService.GetClient(id);
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
        public ActionResult Create([FromBody] NewClientDto newClient)
        {
            if (ModelState.IsValid)
            {
                if (newClient.Id > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                _clientService.AddClient(newClient);
                return Ok();
            }
            return BadRequest();
        }
    }
}
