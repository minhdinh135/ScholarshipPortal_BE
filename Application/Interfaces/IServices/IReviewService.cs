using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Review;

namespace Application.Interfaces.IServices
{
	public interface IReviewService
	{
		Task<IEnumerable<ApplicationReview>> GetAll();
		Task<ApplicationReview> Get(int keys);
		Task<ApplicationReview> Add(AddReviewDTO dto);
		Task<ApplicationReview> Update(UpdateReviewDTO dto);
		Task<ApplicationReview> Delete(int keys);
	}
}
