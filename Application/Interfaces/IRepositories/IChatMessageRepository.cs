using Domain.Entities;

namespace Application.Interfaces.IRepositories;

public interface IChatMessageRepository : IGenericRepository<Chat>
{
    Task SaveMessageAsync(Chat message);
    Task<List<Chat>> GetChatHistoryAsync(int userId, int contactId);
    Task SaveChangesAsync();
    Task<IEnumerable<Chat>> GetMessagesByReceiverId(int receiverId);
}