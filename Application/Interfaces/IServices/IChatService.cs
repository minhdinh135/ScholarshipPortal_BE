﻿using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
	public interface IChatService
	{
		Task<string> SendMessage(int senderId, int receiverId, string message);
		Task SaveMessageAsync(Chat message);
		Task<List<Chat>> GetChatHistoryAsync(int userId, int contactId);
		Task<IEnumerable<Chat>> GetAllMessagesAsync(int receiverId);
	}
}
