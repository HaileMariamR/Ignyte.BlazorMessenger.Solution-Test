using Ignyte.BlazorMessenger.DataLayer.Interface;

namespace Ignyte.BlazorMessenger.DataLayer.Models;

public class Message : IMessage
{
    public string Id { get; set; } = string.Empty;

    // RELATIONSHIP: A Message belongs to a specific ChatRoom
    public string ChatRoomId { get; set; } = string.Empty;

    // RELATIONSHIP: A Message is sent by a specific User
    public string UserId { get; set; } = string.Empty;
    public DateTime DateTimeSent { get; set; } = DateTime.UtcNow;
    public string DisplayName { get; set; } = string.Empty;
    public string NameIdentifier { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
    public string ProfilePicture { get; set; } =string.Empty;
    public bool WasSent { get; set; }
    public bool IsScheduled { get; set; }
    public DateTime? ScheduledFor { get; set; }
    public MessageStatus Status { get; set; } = MessageStatus.Sent;

    public Message() { }

    public Message(string text, string nameIdentifier, string displayName, string chatRoomId, bool isScheduled = false)
    {
        Text = text;
        NameIdentifier = nameIdentifier;
        DisplayName = displayName;
        ChatRoomId = chatRoomId;
        IsScheduled = isScheduled;

        if (isScheduled)
        {
            Status = MessageStatus.Scheduled;
            ScheduledFor = DateTime.UtcNow.AddMinutes(5);
        }
        else
        {
            WasSent = true;
        }
    }
}