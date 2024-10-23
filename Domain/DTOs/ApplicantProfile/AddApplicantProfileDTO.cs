﻿namespace Domain.DTOs.ApplicantProfile
{
	public class AddApplicantProfileDTO
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public DateTime? BirthDate { get; set; }
		public string? Gender { get; set; }
		public string? Nationality { get; set; }
		public string? Ethnicity { get; set; }
		public string? Avatar { get; set; }
		public int? ApplicantId { get; set; }
	}
}
