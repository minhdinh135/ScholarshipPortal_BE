using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Chat
{
	public class ChatMessageDto
	{
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public string Message { get; set; }
		public DateTime Timestamp { get; set; }
	}

	public class ChatHistoryRequest
	{
		public int UserId { get; set; }
		public int ContactId { get; set; }
	}

	public class SendMessageRequest
	{
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public string Message { get; set; }
	}

}
