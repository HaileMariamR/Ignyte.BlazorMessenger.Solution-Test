using Ignyte.BlazorMessenger.DataLayer.Interface;
using Ignyte.BlazorMessenger.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ignyte.BlazorMessenger.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController(IChatServices chatService) : ControllerBase
    {
        private readonly IChatServices _chatService = chatService;

        /// <summary>
        /// Creates a new chat room.
        /// </summary>
        [HttpPost("chatrooms")]
        public async Task<IActionResult> CreateChatRoom([FromBody] ChatRoom chatRoom)
        {
            var result = await _chatService.CreateChatRoomAsync(chatRoom);
            return result ? Ok(new { message = "Chat room created successfully." })
                          : BadRequest("Failed to create chat room.");
        }

        /// <summary>
        /// Gets all chat rooms with their last message.
        /// </summary>
        [HttpGet("chatrooms")]
        public async Task<IActionResult> GetAllChatRooms()
        {
            var chatRooms = await _chatService.GetAllChatRoomsAsync();
            return Ok(chatRooms);
        }

        /// <summary>
        /// Gets a chat room with all its messages.
        /// </summary>
        [HttpGet("chatrooms/{chatRoomId}")]
        public async Task<IActionResult> GetChatRoom(string chatRoomId)
        {
            var chatRoom = await _chatService.GetChatRoomWithMessagesAsync(chatRoomId);
            return chatRoom is not null ? Ok(chatRoom) : NotFound("Chat room not found.");
        }

        /// <summary>
        /// Gets all chat rooms for a specific user.
        /// </summary>
        [HttpGet("chatrooms/user/{userId}")]
        public async Task<IActionResult> GetUserChatRooms(string userId)
        {
            var chatRooms = await _chatService.GetUserChatRoomsAsync(userId);
            return Ok(chatRooms);
        }

        /// <summary>
        /// Adds a message to a chat room.
        /// </summary>
        [HttpPost("chatrooms/{chatRoomId}/messages")]
        public async Task<IActionResult> AddMessage(string chatRoomId, [FromBody] Message message)
        {
            var result = await _chatService.AddMessageAsync(message, chatRoomId);
            return result ? Ok(new { message = "Message added successfully." })
                          : BadRequest("Failed to add message.");
        }

        /// <summary>
        /// Removes a message by its ID.
        /// </summary>
        [HttpDelete("messages/{messageId}")]
        public async Task<IActionResult> RemoveMessage(string messageId)
        {
            var result = await _chatService.RemoveMessageAsync(messageId);
            return result ? Ok(new { message = "Message removed successfully." })
                          : NotFound("Message not found.");
        }
    }
}
