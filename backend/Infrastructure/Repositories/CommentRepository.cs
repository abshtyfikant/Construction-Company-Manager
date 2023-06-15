using Domain.Interfaces.Repository;
using Domain.Model;

namespace Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CommentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int AddComment(Comment comment)
    {
        if (!_dbContext.Services.Any(s => s.Id == comment.ServiceId)) throw new Exception("Service not found");
        if (!_dbContext.Users.Any(s => s.Id == comment.UserId)) throw new Exception("User not found");
        _dbContext.Comments.Add(comment);
        _dbContext.SaveChanges();
        return comment.Id;
    }

    public void DeleteComment(int commentId)
    {
        var comment = _dbContext.Comments.Find(commentId);
        if (comment is not null)
        {
            _dbContext.Comments.Remove(comment);
            _dbContext.SaveChanges();
        }
    }

    public IQueryable<Comment> GetAllComments()
    {
        var comments = _dbContext.Comments;
        return comments;
    }

    public Comment GetComment(int commentId)
    {
        var comment = _dbContext.Comments.FirstOrDefault(i => i.Id == commentId);
        return comment;
    }

    public IQueryable<Comment> GetCommentsByService(int serviceId)
    {
        if (!_dbContext.Services.Any(s => s.Id == serviceId)) throw new Exception("Service not found");
        var comments = _dbContext.Comments.Where(m => m.ServiceId == serviceId);
        return comments;
    }

    public IQueryable<Comment> GetCommentsByUser(Guid userId)
    {
        if (_dbContext.Users.Any(s => s.Id == userId)) throw new Exception("User not found");
        var comments = _dbContext.Comments.Where(m => m.UserId == userId);
        return comments;
    }

    public void UpdateComment(Comment comment)
    {
        _dbContext.Attach(comment);
        _dbContext.Entry(comment).Property("Content").IsModified = true;
        _dbContext.SaveChanges();
    }
}