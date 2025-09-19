namespace Ignyte.BlazorMessenger.UI.Models;

public class ChatRoom
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public List<string> MemberIds { get; set; } = [];
    public List<Message> Messages { get; set; } = [];
    public string CreatedById { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ChatRoom() { }

    public ChatRoom(string name, string createdById)
    {
        Name = name;
        CreatedById = createdById;
        MemberIds.Add(createdById);
    }
}


