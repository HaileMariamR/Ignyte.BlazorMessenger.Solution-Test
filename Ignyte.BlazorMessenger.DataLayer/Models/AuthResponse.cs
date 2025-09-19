namespace Ignyte.BlazorMessenger.DataLayer.Models;

public class AuthResponse
{
    public bool Success { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime Expiration { get; set; }
    public User User { get; set; } = new User();
    public string ErrorMessage { get; set; } = string.Empty;
}