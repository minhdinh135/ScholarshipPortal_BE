using Domain.DTOs.University;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
	public interface IUniversityService
	{
		Task<IEnumerable<University>> GetAll();
		Task<University> Get(int id);
		Task<University> Add(AddUniversityDTO dto);
		Task<University> Update(UpdateUniversityDTO dto);
		Task<University> Delete(int id);
	}
}
