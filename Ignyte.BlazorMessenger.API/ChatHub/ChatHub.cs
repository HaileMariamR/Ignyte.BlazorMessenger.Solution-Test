using Ignyte.BlazorMessenger.DataLayer.Models;
using Microsoft.AspNetCore.SignalR;

namespace Ignyte.BlazorMessenger.API.ChatHub;

public class ChatHub : Hub
{

    public async Task SendMessage(Message message, string userName)
    {
        await Clients.All.SendAsync("ReceiveMessage", message, userName);
    }
    public async Task ChatNotification(string message, List<Guid> recieversList, Guid threadId, Guid senderId)
    {
        await Clients.All.SendAsync("ReceiveChatNotification", message, recieversList, threadId, senderId);
    }
}
