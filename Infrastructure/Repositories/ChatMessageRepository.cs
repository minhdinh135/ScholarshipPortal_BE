using Application.Interfaces.IRepositories;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ChatMessageRepository : GenericRepository<Chat>, IChatMessageRepository
    {
        public ChatMessageRepository(ScholarshipContext dbContext) : base(dbContext)
        {
        }

        public async Task SaveMessageAsync(Chat message)
        {
            await _dbContext.Chats.AddAsync(message);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Chat>> GetChatHistoryAsync(int userId, int contactId)
        {
            return await _dbContext.Chats
                .Where(m => (m.SenderId == userId && m.ReceiverId == contactId) ||
                            (m.SenderId == contactId && m.ReceiverId == userId))
                .OrderBy(m => m.SentDate)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chat>> GetMessagesByReceiverId(int receiverId)
        {
            return await _dbContext.Chats
                .Where(chat => chat.ReceiverId == receiverId)
                .ToListAsync();
        }
    }
}
