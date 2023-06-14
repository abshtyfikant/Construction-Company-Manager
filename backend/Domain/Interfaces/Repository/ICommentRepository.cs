using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repository
{
    public interface ICommentRepository
    {
        void DeleteComment(int commentId);
        int AddComment(Comment comment);
        IQueryable<Comment> GetAllComments();
        IQueryable<Comment> GetCommentsByService(int serviceId);
        IQueryable<Comment> GetCommentsByUser(Guid userId);
        Comment GetComment(int commentId);
        void UpdateComment(Comment comment);
    }
}

