using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
	public class FeedbackDTO
	{
		public string? Content { get; set; }
		public double? Rating { get; set; }
		public DateTime? FeedbackDate { get; set; }
		public int? FunderId { get; set; }
		public int? ProviderId { get; set; }
	}
}
