namespace AuthService.API.DTOs;

public class AuthResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
