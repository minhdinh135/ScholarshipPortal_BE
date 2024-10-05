using Domain.DTOs.Application;
using Domain.Entities;

namespace Application.Interfaces.IServices
{
	public interface IApplicationsService
	{
		Task<IEnumerable<Domain.Entities.Application>> GetAll();
		Task<Domain.Entities.Application> Get(int id);
		Task<Domain.Entities.Application> Add(ApplicationAddDTO dto);
		Task<Domain.Entities.Application> Update(ApplicationUpdateDTO dto);
		Task<Domain.Entities.Application> Delete(int id);
	}
}
