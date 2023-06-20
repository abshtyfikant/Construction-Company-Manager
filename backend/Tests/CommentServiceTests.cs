using Application.DTO.Comment;
using Application.Mapping;
using Application.Services;
using AutoMapper;
using Domain.Interfaces.Repository;
using Domain.Model;
using Moq;

namespace Tests;

public class CommentServiceTests
{
    private readonly Mock<ICommentRepository> _commentRepositoryMock;
    private readonly IMapper _mapper;

    public CommentServiceTests()
    {
        _commentRepositoryMock = new Mock<ICommentRepository>();
        var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
        _mapper = mockMapper.CreateMapper();
    }

    [Fact]
    public void AddComment_ShouldReturnId()
    {
        // Arrane
        var commentService = new CommentService(_mapper, _commentRepositoryMock.Object);
        var comment = new NewCommentDto
        {
            Id = 1,
            ServiceId = 1,
            Content = "test",
            Date = DateTime.Now,
            UserId = Guid.NewGuid()
        };
        _commentRepositoryMock.Setup(x => x.AddComment(It.IsAny<Comment>())).Returns(1);

        // Act
        var result = commentService.AddComment(comment);

        // Assert
        Assert.Equal(1, result);
    }

    [Fact]
    public void DeleteComment_ShouldBeCalledOnce()
    {
        // Arrange
        var commentService = new CommentService(_mapper, _commentRepositoryMock.Object);
        var commentId = 1;

        // Act
        commentService.DeleteComment(commentId);

        // Assert
        _commentRepositoryMock.Verify(x => x.DeleteComment(commentId), Times.Once);
    }

    [Fact]
    public void GetComment_ShouldReturnComment()
    {
        // Arrange
        var commentService = new CommentService(_mapper, _commentRepositoryMock.Object);
        var comment = new Comment
        {
            Id = 1,
            ServiceId = 1,
            Content = "test",
            Date = DateTime.Now,
            UserId = Guid.NewGuid()
        };
        _commentRepositoryMock.Setup(x => x.GetComment(comment.Id)).Returns(comment);

        // Act
        var result = commentService.GetComment(comment.Id);

        // Assert
        Assert.Equal(comment.Id, result.Id);
        Assert.Equal(comment.ServiceId, result.ServiceId);
        Assert.Equal(comment.Content, result.Content);
        Assert.Equal(comment.Date, result.Date);
        Assert.Equal(comment.UserId, result.UserId);
    }

    [Fact]
    public void UpdateComment_ShouldReturnComment()
    {
        // Arrange
        var commentService = new CommentService(_mapper, _commentRepositoryMock.Object);
        var comment = new NewCommentDto
        {
            Id = 1,
            ServiceId = 1,
            Content = "test",
            Date = DateTime.Now,
            UserId = Guid.NewGuid()
        };

        // Act
        var result = commentService.UpdateComment(comment);

        // Assert
        Assert.Equal(comment.Id, result.Id);
        Assert.Equal(comment.ServiceId, result.ServiceId);
        Assert.Equal(comment.Content, result.Content);
        Assert.Equal(comment.Date, result.Date);
        Assert.Equal(comment.UserId, result.UserId);
    }

    [Fact]
    public void GetCommentsByServiceForList_ShouldReturnComments()
    {
        // Arrange
        var commentService = new CommentService(_mapper, _commentRepositoryMock.Object);
        var comments = new List<Comment>
        {
            new Comment
            {
                Id = 1,
                ServiceId = 1,
                Content = "test",
                Date = DateTime.Now,
                User = new User(),
                UserId = Guid.NewGuid()
            },
            new Comment
            {
                Id = 2,
                ServiceId = 1,
                Content = "test",
                Date = DateTime.Now,
                User = new User(),
                UserId = Guid.NewGuid()
            },
        };
        _commentRepositoryMock.Setup(x => x.GetCommentsByService(It.IsAny<int>()))
            .Returns(comments.AsQueryable());

        // Act
        var result = commentService.GetCommentsByServiceForList(1);

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetCommentsByUserForList_ShouldReturnComments()
    {
        // Arrange
        var commentService = new CommentService(_mapper, _commentRepositoryMock.Object);
        var comments = new List<Comment>
        {
            new Comment
            {
                Id = 1,
                ServiceId = 1,
                Content = "test",
                Date = DateTime.Now,
                User = new User(),
                UserId = Guid.NewGuid()
            },
            new Comment
            {
                Id = 2,
                ServiceId = 1,
                Content = "test",
                Date = DateTime.Now,
                User = new User(),
                UserId = Guid.NewGuid()
            },
        };
        var guid = new Guid();
        _commentRepositoryMock.Setup(x => x.GetCommentsByUser(It.IsAny<Guid>()))
            .Returns(comments.AsQueryable());

        // Act
        var result = commentService.GetCommentsByUserForList(guid);

        // Assert
        Assert.Equal(2, result.Count);
    }


    [Fact]
    public void GetCommentsForList_ShouldReturnComments()
    {
        // Arrange
        var commentService = new CommentService(_mapper, _commentRepositoryMock.Object);
        var comments = new List<Comment>
        {
            new Comment
            {
                Id = 1,
                ServiceId = 1,
                Content = "test",
                Date = DateTime.Now,
                User = new User(),
                UserId = Guid.NewGuid()
            },
            new Comment
            {
                Id = 2,
                ServiceId = 1,
                Content = "test",
                Date = DateTime.Now,
                User = new User(),
                UserId = Guid.NewGuid()
            },
        };
        _commentRepositoryMock.Setup(x => x.GetAllComments()).Returns(comments.AsQueryable);

        // Act
        var result = commentService.GetCommentsForList();

        // Assert
        Assert.Equal(2, result.Count);
    }
}