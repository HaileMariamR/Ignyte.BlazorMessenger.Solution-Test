namespace Ignyte.BlazorMessenger.DataLayer.Models;

public class User
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string DisplayName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ProfilePicture { get; set; } = string.Empty;
    public bool IsOnline { get; set; }
    public List<string> ChatRoomIds { get; set; } = [];

    public User() { }

    public User(string displayName, string email, string profilePicture = "")
    {
        DisplayName = displayName;
        Email = email;
        ProfilePicture = profilePicture;
    }
}
