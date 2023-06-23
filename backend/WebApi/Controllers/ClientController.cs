using Application.DTO.Client;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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

    [HttpGet("{id:int}", Name = "GetClient")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ClientDto> Get(int id)
    {
        if (id == 0) return BadRequest();
        var service = _clientService.GetClient(id);
        if (service is null) return NotFound();
        return Ok(service);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Create([FromBody] NewClientDto newClient)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (newClient.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);
        var clientId = _clientService.AddClient(newClient);
        newClient.Id = clientId;
        return CreatedAtRoute("GetClient", new { id = clientId }, newClient);
    }

    [HttpDelete("{id:int}", Name = "DeleteClient")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Delete(int id)
    {
        if (id == 0) return BadRequest();
        _clientService.DeleteClient(id);
        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromBody] NewClientDto newClient)
    {
        if (!ModelState.IsValid) return BadRequest();
        if (newClient.Id <= 0) return BadRequest();
        _clientService.UpdateClient(newClient);
        return NoContent();
    }
}