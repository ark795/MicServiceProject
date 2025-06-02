using AuthService.API.Models;

namespace AuthService.API.Services;

public interface ITokenService
{
    string CreateAccessToken(User user);
    string CreateRefreshToken();
}
