using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Country;

namespace Application.Interfaces.IServices
{
	public interface ICountryService
	{
		Task<IEnumerable<Country>> GetAll();
		Task<Country> Get(int id);
		Task<Country> Add(AddCountryDTO dto);
		Task<Country> Update(UpdateCountryDTO dto);
		Task<Country> Delete(int id);
	}
}
