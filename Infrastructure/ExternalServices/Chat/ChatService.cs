using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
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
				        { "SentDate", DateTime.Now.ToString("o") }
			        },
					Topic = $"{receiverId}"
				};

				string response = await FirebaseMessaging.DefaultInstance.SendAsync(messageData);

				_logger.LogInformation($"Message sent successfully from {senderId} to {receiverId}. Response: {response}");

				var chatMessage = new Domain.Entities.Chat
				{
					SenderId = senderId,
					ReceiverId = receiverId,
					Message = message,
					SentDate = DateTime.Now,
					IsRead = false
				};

				await SaveMessageAsync(chatMessage);

				return response;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "An error occurred while sending the message.");
				throw;
			}
		}

		public async Task SaveMessageAsync(Domain.Entities.Chat message)
		{
			message.IsRead = false; 
			await _chatMessageRepository.SaveMessageAsync(message);
		}


		public async Task<List<Domain.Entities.Chat>> GetChatHistoryAsync(int userId, int contactId)
		{
			var chatHistory = await _chatMessageRepository.GetChatHistoryAsync(userId, contactId);

			foreach (var message in chatHistory.Where(m => m.ReceiverId == userId || m.IsRead == false))
			{
				message.IsRead = true;
			}

			await _chatMessageRepository.SaveChangesAsync();

			foreach (var message in chatHistory)
			{
				message.SentDate = DateTime.SpecifyKind(message.SentDate, DateTimeKind.Local);
			}

			return chatHistory;
		}


		public async Task<IEnumerable<Domain.Entities.Chat>> GetAllMessagesAsync(int receiverId)
		{
			return await _chatMessageRepository.GetMessagesByReceiverId(receiverId);
		}

	}
}
