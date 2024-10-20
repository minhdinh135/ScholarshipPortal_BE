using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
	public class ChatMessageRepository : IChatMessageRepository
	{
		private readonly ScholarshipContext _context;

		public ChatMessageRepository(ScholarshipContext context)
		{
			_context = context;
		}

		public async Task SaveMessageAsync(ChatMessage message)
		{
			await _context.ChatMessages.AddAsync(message);
			await _context.SaveChangesAsync();
		}

		public async Task<List<ChatMessage>> GetChatHistoryAsync(int userId, int contactId)
		{
			return await _context.ChatMessages
				.Where(m => (m.SenderId == userId && m.ReceiverId == contactId) ||
							 (m.SenderId == contactId && m.ReceiverId == userId))
				.OrderBy(m => m.Timestamp)
				.ToListAsync();
		}
	}
}
