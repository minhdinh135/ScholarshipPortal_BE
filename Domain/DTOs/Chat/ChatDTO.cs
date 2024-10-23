namespace Domain.DTOs.Chat
{
	public class ChatMessageDto
	{
		public int SenderId { get; set; }
		public int ReceiverId { get; set; }
		public string Message { get; set; }
		public DateTime? SentDate { get; set; }
		public bool IsRead { get; set; }
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
