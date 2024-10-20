using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class ChatMessage
	{
		public int Id { get; set; }
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public string Message { get; set; }
		public DateTime Timestamp { get; set; }
	}
}
