using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.DTOs.Chat;
using Domain.Entities;
using FirebaseAdmin.Messaging;
using Microsoft.Extensions.Logging;

namespace Infrastructure.ExternalServices.Chat
{
	public class ChatService : IChatService
	{
		private readonly ILogger<ChatService> _logger;
		private readonly IChatMessageRepository _chatMessageRepository;

		public ChatService(ILogger<ChatService> logger, IChatMessageRepository chatMessageRepository)
		{
			_logger = logger;
			_chatMessageRepository = chatMessageRepository;
		}

		public async Task<string> SendMessage(int senderId, int receiverId, string message)
		{
			try
			{
				var messageData = new Message()
				{
					Data = new Dictionary<string, string>()
			{
				{ "SenderId", senderId.ToString() },
				{ "ReceiverId", receiverId.ToString() },
				{ "Message", message },
				{ "Timestamp", DateTime.UtcNow.ToString("o") }
			},
					Topic = $"{receiverId}"
				};

				string response = await FirebaseMessaging.DefaultInstance.SendAsync(messageData);

				_logger.LogInformation($"Message sent successfully from {senderId} to {receiverId}. Response: {response}");

				// Save the message to the database
				var chatMessage = new ChatMessage
				{
					SenderId = senderId,
					ReceiverId = receiverId,
					Message = message,
					Timestamp = DateTime.UtcNow
				};

				await SaveMessageAsync(chatMessage);

				return response;
			}
			catch (FirebaseMessagingException ex)
			{
				_logger.LogError(ex, "Firebase messaging error while sending message.");
				throw new Exception("Failed to send message via Firebase.");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An unexpected error occurred while sending the message.");
				throw new Exception("An unexpected error occurred while sending the message.");
			}
		}


		public async Task SaveMessageAsync(ChatMessage message)
		{
			await _chatMessageRepository.SaveMessageAsync(message);
		}

	}
}
