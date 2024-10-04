using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
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
