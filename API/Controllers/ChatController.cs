using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.DTOs.Chat;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/chat")]
	public class ChatController : ControllerBase
	{
		private readonly IChatService _chatService;
		private readonly IAccountsService _accountsService;
		private readonly IChatMessageRepository _chatMessageRepository;

		public ChatController(IChatService chatService, IAccountsService accountsService, IChatMessageRepository chatMessageRepository)
		{
			_chatService = chatService;
			_accountsService = accountsService;
			_chatMessageRepository = chatMessageRepository;
		}

		[HttpPost("send-message")]
		public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
		{
			var users = await _accountsService.GetAll();

			var sender = users.FirstOrDefault(u => u.Id == request.SenderId);
			if (sender == null)
			{
				return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Sender ID not found", null));
			}

			var receiver = users.FirstOrDefault(u => u.Id == request.ReceiverId);
			if (receiver == null)
			{
				return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Receiver ID not found", null));
			}

			var response = await _chatService.SendMessage(request.SenderId, request.ReceiverId, request.Message);

			return Ok(new ApiResponse(StatusCodes.Status200OK, "Message sent successfully", response));
		}

		[HttpPost("history")]
		public async Task<IActionResult> GetChatHistory([FromBody] ChatHistoryRequest request)
		{
			var chatHistory = await _chatService.GetChatHistoryAsync(request.UserId, request.ContactId);

			return Ok(new ApiResponse(StatusCodes.Status200OK, "Chat history retrieved successfully", chatHistory));
		}

		[HttpGet("all-messages/{receiverId}")]
		public async Task<IActionResult> GetAllMessages(int receiverId)
		{
			var messages = await _chatService.GetAllMessagesAsync(receiverId);

			if (messages == null || !messages.Any())
			{
				return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "No messages found for the specified receiver", null));
			}

			return Ok(new ApiResponse(StatusCodes.Status200OK, "Messages retrieved successfully", messages));
		}
	}
}
