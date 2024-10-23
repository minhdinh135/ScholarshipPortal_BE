using Domain.DTOs.Account;
using Domain.DTOs.Achievement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.ApplicantProfile
{
	public class ApplicantProfileDTO
	{
		public int Id { get; set; }

		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public DateTime? BirthDate { get; set; }

		public string? Gender { get; set; }

		public string? Nationality { get; set; }

		public string? Ethnicity { get; set; }

		public int? ApplicantId { get; set; }
	}
}
