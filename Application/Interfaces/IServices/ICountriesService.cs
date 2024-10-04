using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
	public interface ICountriesService
	{
		Task<IEnumerable<Country>> GetAll();
		Task<Country> Get(int id);
		Task<Country> Add(AddCountryDTO dto);
		Task<Country> Update(UpdateCountryDTO dto);
		Task<Country> Delete(int id);
	}
}
