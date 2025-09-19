using Ignyte.BlazorMessenger.DataLayer.Models;

namespace Ignyte.BlazorMessenger.DataLayer.Interface;

public interface IAuthService
{
    Task<AuthResponse> AuthenticateUserAsync(AuthRequest authRequest);
    Task<User?> GetUserAsync(string userId);
    Task<List<User>> GetUsersAsync();
    Task<bool> IsAuthenticatedAsync();
    Task LogoutAsync();
    Task<User?> GetCurrentUserAsync();
}
