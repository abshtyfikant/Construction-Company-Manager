using Application.DTO.Comment;

namespace Application.Interfaces.Services;

public interface ICommentService
{
    int AddComment(NewCommentDto comment);
    List<CommentDto> GetCommentsForList();
    List<CommentDto> GetCommentsByServiceForList(int serviceId);
    List<CommentDto> GetCommentsByUserForList(Guid userId);
    object GetComment(int commentId);
    object UpdateComment(NewCommentDto newComment);
    void DeleteComment(int commentId);
}