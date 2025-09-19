

using Ignyte.BlazorMessenger.DataLayer.DatabaseContext;
using Ignyte.BlazorMessenger.DataLayer.Interface;
using Ignyte.BlazorMessenger.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Ignyte.BlazorMessenger.DataLayer.Services;

public class AuthServices(ChatDatabaseContext context) : IAuthService
{

    private readonly ChatDatabaseContext _context = context;

    private User? _currentUser;

    /// <summary>
    /// Authenticate a user by username and password.
    /// </summary>
    public async Task<AuthResponse> AuthenticateUserAsync(AuthRequest authRequest)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.DisplayName == authRequest.Username);
        if (user == null)
        {
            return new AuthResponse { Success = false, User = null };
        }

        if (user.Password == authRequest.Password)
        {
            _currentUser = user;
            return new AuthResponse { Success = true, User = user };
        }

        return new AuthResponse { Success = false, User = null };
    }

    /// <summary>
    /// Get a user by Id.
    /// </summary>
    public async Task<User?> GetUserAsync(string userId)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }
    /// <summary>
    /// Get all users.
    /// </summary>
    public async Task<List<User>> GetUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    /// <summary>
    /// Get the currently authenticated user.
    /// </summary>
    public Task<User?> GetCurrentUserAsync()
    {
        return Task.FromResult(_currentUser);
    }

    /// <summary>
    /// Check if a user is authenticated.
    /// </summary>
    public Task<bool> IsAuthenticatedAsync()
    {
        return Task.FromResult(_currentUser != null);
    }

    /// <summary>
    /// Logout the current user.
    /// </summary>
    public Task LogoutAsync()
    {
        _currentUser = null;
        return Task.CompletedTask;
    }
}
