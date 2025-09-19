using Ignyte.BlazorMessenger.DataLayer.Models;

namespace Ignyte.BlazorMessenger.DataLayer.Interface;

public interface IChatServices
{
    Task<bool> AddMessageAsync(Message message, string chatRoomId);
    Task<bool> RemoveMessageAsync(string messageId);
    Task<bool> CreateChatRoomAsync(ChatRoom chatRoom);
    Task<List<ChatRoom>> GetAllChatRoomsAsync();
    Task<ChatRoom?> GetChatRoomWithMessagesAsync(string chatRoomId);
    Task<List<ChatRoom>> GetUserChatRoomsAsync(string userId);
}
