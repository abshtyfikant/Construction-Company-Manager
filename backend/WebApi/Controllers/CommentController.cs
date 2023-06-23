using System.Security.Claims;
using Application.DTO.Comment;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentservice;

    public CommentController(ICommentService commentservice)
    {
        _commentservice = commentservice;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<CommentDto>> Get()
    {
        var list = _commentservice.GetCommentsForList();
        return Ok(list);
    }

    [HttpGet("{id:int}", Name = "GetComment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<CommentDto> Get(int id)
    {
        if (id == 0) return BadRequest();
        var comment = _commentservice.GetComment(id);
        if (comment is null) return NotFound();
        return Ok(comment);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult Create([FromBody] NewCommentDto newComment)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (newComment.Id > 0) return StatusCode(StatusCodes.Status500InternalServerError);
        newComment.UserId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var id = _commentservice.AddComment(newComment);
        newComment.Id = id;
        return CreatedAtRoute("GetComment", new { id }, newComment);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Update([FromBody] NewCommentDto newComment)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        if (newComment.Id <= 0) return BadRequest(ModelState);
        _commentservice.UpdateComment(newComment);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Delete(int id)
    {
        if (id <= 0) return BadRequest();
        _commentservice.DeleteComment(id);
        return NoContent();
    }

    [HttpGet($"{nameof(GetByService)}/{{serviceId:int}}", Name = "GetCommentByService")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<IEnumerable<CommentDto>> GetByService(int serviceId)
    {
        if (serviceId == 0) return BadRequest();
        var list = _commentservice.GetCommentsByServiceForList(serviceId);
        return Ok(list);
    }

    [HttpGet($"{nameof(GetByUser)}/{{userId:Guid}}", Name = "GetCommentByUser")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<CommentDto>> GetByUser(Guid userId)
    {
        var list = _commentservice.GetCommentsByUserForList(userId);
        return Ok(list);
    }
}