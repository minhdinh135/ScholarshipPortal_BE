using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.University
{
	public class AddUniversityDTO
	{
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? City { get; set; }
		public int? CountryId { get; set; }
	}

	public class UpdateUniversityDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? City { get; set; }
		public int? CountryId { get; set; }
	}
}
