using Application.DTO.Comment;

namespace Application.Interfaces.Services;

public interface ICommentService
{
    int AddComment(NewCommentDto comment);
    List<CommentDto> GetCommentsForList();
    List<CommentDto> GetCommentsByServiceForList(int serviceId);
    List<CommentDto> GetCommentsByUserForList(Guid userId);
    CommentDto GetComment(int commentId);
    NewCommentDto UpdateComment(NewCommentDto newComment);
    void DeleteComment(int commentId);
}