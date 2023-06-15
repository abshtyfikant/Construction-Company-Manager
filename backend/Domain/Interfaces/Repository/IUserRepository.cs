using Domain.Model;

namespace Domain.Interfaces.Repository;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}