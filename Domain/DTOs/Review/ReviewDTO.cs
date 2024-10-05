namespace Domain.DTOs.Review
{
	public class UpdateReviewDTO
	{
		public int Id { get; set; }

		public DateTime? CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }

		public string? Status { get; set; }
		public string? Comment { get; set; }
		public double? Score { get; set; }
		public DateTime? ReviewedDate { get; set; }
		public int? ProviderId { get; set; }
		public int? ApplicationId { get; set; }
	}

	public class AddReviewDTO
	{
		public string? Comment { get; set; }
		public double? Score { get; set; }
		public DateTime? ReviewedDate { get; set; }
		public int? ProviderId { get; set; }
		public int? ApplicationId { get; set; }
		public DateTime? CreatedAt { get; set; }

		public DateTime? UpdatedAt { get; set; }

		public string? Status { get; set; }
	}
}
