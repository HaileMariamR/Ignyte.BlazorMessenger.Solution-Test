using Ignyte.BlazorMessenger.DataLayer.DatabaseContext;
using Ignyte.BlazorMessenger.DataLayer.Interface;
using Ignyte.BlazorMessenger.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Ignyte.BlazorMessenger.DataLayer.Services;

/// <summary>
/// Service layer for handling chat operations such as messages and chat rooms.
/// </summary>
public class ChatServices(ChatDatabaseContext _context) : IChatServices
{
    /// <summary>
    /// Adds a new message to a given chat room.
    /// </summary>
    /// <param name="message">The message to add.</param>
    /// <param name="chatRoomId">The ID of the chat room where the message belongs.</param>
    /// <returns>True if added successfully, false otherwise.</returns>
    public async Task<bool> AddMessageAsync(Message message, string chatRoomId)
    {
        try
        {
            // Find the chat room
            var chatRoom = await _context.ChatRooms
                .FirstOrDefaultAsync(cr => cr.Id == chatRoomId);

            if (chatRoom == null) return false;

            // Set required fields for the message
            message.ChatRoomId = chatRoomId;
            message.DateTimeSent = DateTime.UtcNow;

            // Add message and save
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error at AddMessageAsync : {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Removes a message by its ID.
    /// </summary>
    /// <param name="messageId">The ID of the message to remove.</param>
    /// <returns>True if removed successfully, false otherwise.</returns>
    public async Task<bool> RemoveMessageAsync(string messageId)
    {
        try
        {
            // Find the message
            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.Id == messageId);

            if (message == null) return false;

            // Remove message and save
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error at RemoveMessageAsync: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Creates a new chat room.
    /// </summary>
    /// <param name="chatRoom">The chat room entity to create.</param>
    /// <returns>True if created successfully, false otherwise.</returns>
    public async Task<bool> CreateChatRoomAsync(ChatRoom chatRoom)
    {
        try
        {
            // Set created date
            chatRoom.CreatedAt = DateTime.UtcNow;

            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error at CreateChatRoomAsync: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Retrieves all chat rooms with their last message.
    /// </summary>
    /// <returns>A list of chat rooms.</returns>
    public async Task<List<ChatRoom>> GetAllChatRoomsAsync()
    {
        try
        {
            return await _context.ChatRooms
                .AsNoTracking()
                // Include only the last message
                .Include(cr => cr.Messages.OrderByDescending(m => m.DateTimeSent).Take(1))
                .OrderByDescending(cr => cr.CreatedAt)
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error at GetAllChatRoomsAsync : {ex.Message}");
            return [];
        }
    }

    /// <summary>
    /// Retrieves a specific chat room along with all its messages.
    /// </summary>
    /// <param name="chatRoomId">The ID of the chat room.</param>
    /// <returns>The chat room with its messages, or null if not found.</returns>
    public async Task<ChatRoom?> GetChatRoomWithMessagesAsync(string chatRoomId)
    {
        try
        {
            return await _context.ChatRooms
                .AsNoTracking()
                // Include all messages ordered by time sent
                .Include(cr => cr.Messages.OrderBy(m => m.DateTimeSent))
                .FirstOrDefaultAsync(cr => cr.Id == chatRoomId);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error at GetChatRoomWithMessagesAsync : {ex.Message}");
            return null;
        }
    }

    /// <summary>
    /// Retrieves all chat rooms for a specific user.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A list of chat rooms that the user is a member of.</returns>
    public async Task<List<ChatRoom>> GetUserChatRoomsAsync(string userId)
    {
        try
        {
            return await _context.ChatRooms
                .AsNoTracking()
                // Find chat rooms where the user is a member
                .Where(cr => cr.MemberIds.Contains(userId))
                // Include the last 50 messages
                .Include(cr => cr.Messages.OrderBy(m => m.DateTimeSent).Take(50))
                // Order by most recent message
                .OrderByDescending(cr => cr.Messages.Max(m => m.DateTimeSent))
                .ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error at GetUserChatRoomsAsync : {ex.Message}");
            return [];
        }
    }
}
