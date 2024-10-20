using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IRepositories
{
	public interface IChatMessageRepository
	{
		Task SaveMessageAsync(ChatMessage message);
		Task<List<ChatMessage>> GetChatHistoryAsync(int userId, int contactId);
	}
}
