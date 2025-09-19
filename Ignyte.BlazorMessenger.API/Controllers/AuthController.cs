using Ignyte.BlazorMessenger.DataLayer.Interface;
using Ignyte.BlazorMessenger.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ignyte.BlazorMessenger.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IAuthService _authService) : ControllerBase
    {

        /// <summary>
        /// Health check endpoint to verify that the Auth service is running.
        /// </summary>
        [HttpGet]
        public ActionResult<string> GetStatus() => "Auth service is running";

        /// <summary>
        /// Retrieves all registered users in the system.
        /// </summary>
        /// <returns>A list of User objects.</returns>
        [HttpGet("users")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _authService.GetUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Retrieves a specific user by their unique ID.
        /// </summary>
        /// <param name="userId">The GUID of the user to fetch.</param>
        /// <returns>The User object if found; otherwise, a 404 NotFound response.</returns>
        [HttpGet("users/{userId:guid}")]
        public async Task<ActionResult<User?>> GetUserById(Guid userId)
        {
            var user = await _authService.GetUserAsync(userId.ToString());
            return user is not null ? Ok(user) : NotFound($"User with ID '{userId}' not found.");
        }

        /// <summary>
        /// Authenticates a user based on the provided login credentials.
        /// </summary>
        /// <param name="authRequest">Contains username and password for authentication.</param>
        /// <returns>
        /// If successful, returns the authenticated User object.
        /// If authentication fails, returns 401 Unauthorized with an error message.
        /// </returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest authRequest)
        {
            var authModel = await _authService.AuthenticateUserAsync(authRequest);

            return authModel.Success switch
            {
                true => Ok(new AuthResponse
                {
                    User = authModel.User,
                }),
                false => Unauthorized(new AuthResponse { Success = false, ErrorMessage = "Invalid username or password" })
            };
        }
    }
}
