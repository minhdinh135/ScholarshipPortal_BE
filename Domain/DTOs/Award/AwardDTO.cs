namespace Domain.DTOs.Award
{
	public class AddAwardDTO
	{
		public string? Description { get; set; }
		public decimal? Amount { get; set; }
		public string? Image { get; set; }
		public DateTime? AwardedDate { get; set; }
		public int? ApplicationId { get; set; }
	}

	public class UpdateAwardDTO
	{
		public int Id { get; set; }
		public string? Description { get; set; }
		public decimal? Amount { get; set; }
		public string? Image { get; set; }
		public DateTime? AwardedDate { get; set; }
		public int? ApplicationId { get; set; }
	}
}
