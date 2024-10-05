using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Feedback;

namespace Application.Interfaces.IServices
{
	public interface IFeedbackService
	{
		Task<IEnumerable<Feedback>> GetAll();
		Task<Feedback> Get(int id);
		Task<Feedback> Add(AddFeedbackDTO dto);
		Task<Feedback> Update(UpdateFeedbackDTO dto);
		Task<Feedback> Delete(int id);
	}
}
