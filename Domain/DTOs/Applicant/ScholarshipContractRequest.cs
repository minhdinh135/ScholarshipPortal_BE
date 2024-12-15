using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Applicant
{
	public class ScholarshipContractRequest
	{
		public string? ApplicantName { get; set; }
		public string? ScholarshipAmount { get; set; }
		public string? ScholarshipProviderName { get; set; }
		public string? ScholarshipProviderSignature { get; set; }
		public DateTime Deadline { get; set; }
	}

}
