using System.Threading;

namespace Ignyte.BlazorMessenger.DataLayer.Interface;

public interface IMessage
{
    string Id { get; set; }
    string ChatRoomId { get; set; }
    DateTime DateTimeSent { get; set; }
    string DisplayName { get; set; }
    string NameIdentifier { get; set; }
    string Text { get; set; }
    string ProfilePicture { get; set; }
    bool WasSent { get; set; }
    bool IsScheduled { get; set; }
    DateTime? ScheduledFor { get; set; }
    MessageStatus Status { get; set; }
}

public enum MessageStatus
{
    Sent,
    Delivered,
    Read,
    Scheduled,
    Failed
}


