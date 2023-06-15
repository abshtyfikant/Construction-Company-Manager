using Domain.Interfaces.Repository;
using Domain.Model;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public User? GetUserByEmail(string email)
    {
        var user = _dbContext.Users.FirstOrDefault(i => i.Email == email);
        return user;
    }
}