namespace Domain.DTOs.Country
{
	public class AddCountryDTO
	{
		public string? Name { get; set; }
		public int? Code { get; set; }
	}

	public class UpdateCountryDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public int? Code { get; set; }
	}
}
