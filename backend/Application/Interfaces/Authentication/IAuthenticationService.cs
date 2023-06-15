using Application.Services.Authentication;

namespace Application.Interfaces.Authentication;

public interface IAuthenticationService
{
    AuthenticationResult Register(string firstName, string lastName, string email, string password);
    AuthenticationResult Login(string email, string password);
}