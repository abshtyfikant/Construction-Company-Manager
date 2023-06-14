using Application.DTO.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services
{
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
}
