using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.ApplicantProfile;

namespace Application.Interfaces.IServices
{
	public interface IApplicantProfileService
	{
		Task<IEnumerable<ApplicantProfile>> GetAll();
		Task<ApplicantProfile> Get(int id);
		Task<ApplicantProfile> Add(AddApplicantProfileDTO dto);
		Task<ApplicantProfile> Update(UpdateApplicantProfileDTO dto);
		Task<ApplicantProfile> Delete(int id);
	}
}
