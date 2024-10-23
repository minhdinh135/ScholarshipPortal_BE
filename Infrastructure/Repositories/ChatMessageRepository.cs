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

		public async Task SaveMessageAsync(Chat message)
		{
			await _context.Chats.AddAsync(message);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Chat>> GetChatHistoryAsync(int userId, int contactId)
		{
			return await _context.Chats
				.Where(m => (m.SenderId == userId && m.ReceiverId == contactId) ||
							(m.SenderId == contactId && m.ReceiverId == userId))
				.OrderBy(m => m.SentDate)
				.ToListAsync();
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}

		public async Task<IEnumerable<Chat>> GetMessagesByReceiverId(int receiverId)
		{
			return await _context.Chats
				.Where(chat => chat.ReceiverId == receiverId)
				.ToListAsync();
		}

	}
}
