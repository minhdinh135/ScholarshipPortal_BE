using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
	public class AddDocumentDTO
	{
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Content { get; set; }
		public string? Type { get; set; }
		public string? FilePath { get; set; }
		public int? ApplicationId { get; set; }
	}

	public class UpdateDocumentDTO
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Content { get; set; }
		public string? Type { get; set; }
		public string? FilePath { get; set; }
		public int? ApplicationId { get; set; }
	}
}
