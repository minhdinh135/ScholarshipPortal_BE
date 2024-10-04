using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
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
