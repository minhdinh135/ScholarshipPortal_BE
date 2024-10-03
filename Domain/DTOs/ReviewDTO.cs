using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
	public class ReviewDTO
	{
		public string? Comment { get; set; }
		public double? Score { get; set; }
		public DateTime? ReviewedDate { get; set; }
		public int? ProviderId { get; set; }
		public int? ApplicationId { get; set; }
	}
}
