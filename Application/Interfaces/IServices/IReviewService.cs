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
		Task<IEnumerable<Review>> GetAll();
		Task<Review> Get(int keys);
		Task<Review> Add(AddReviewDTO dto);
		Task<Review> Update(UpdateReviewDTO dto);
		Task<Review> Delete(int keys);
	}
}
