using Application.DTO.Comment;
using Application.Interfaces.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Interfaces.Repository;
using Domain.Model;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(IMapper mapper, ICommentRepository commentRepository)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
    }

    public int AddComment(NewCommentDto comment)
    {
        var commentEntity = _mapper.Map<Comment>(comment);
        var id = _commentRepository.AddComment(commentEntity);
        return id;
    }

    public void DeleteComment(int commentId)
    {
        _commentRepository.DeleteComment(commentId);
    }

    public CommentDto GetComment(int commentId)
    {
        var comment = _commentRepository.GetComment(commentId);
        var commentDto = _mapper.Map<CommentDto>(comment);
        return commentDto;
    }

    public List<CommentDto> GetCommentsByServiceForList(int serviceId)
    {
        var comments = _commentRepository.GetCommentsByService(serviceId)
            .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
            .ToList();
        return comments;
    }

    public List<CommentDto> GetCommentsByUserForList(Guid userId)
    {
        var comments = _commentRepository.GetCommentsByUser(userId)
            .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
            .ToList();
        return comments;
    }

    public List<CommentDto> GetCommentsForList()
    {
        var comments = _commentRepository.GetAllComments()
            .ProjectTo<CommentDto>(_mapper.ConfigurationProvider)
            .ToList();
        return comments;
    }

    public NewCommentDto UpdateComment(NewCommentDto newComment)
    {
        var commentEntity = _mapper.Map<Comment>(newComment);
        _commentRepository.UpdateComment(commentEntity);
        return newComment;
    }
}