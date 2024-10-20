using Domain.DTOs.Chat;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
	public interface IChatService
	{
		Task<string> SendMessage(int senderId, int receiverId, string message);
		Task SaveMessageAsync(ChatMessage message);
	}
}
