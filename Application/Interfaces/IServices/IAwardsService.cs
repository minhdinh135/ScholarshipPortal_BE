﻿using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
	public interface IAwardsService
	{
		Task<IEnumerable<Award>> GetAll();
		Task<Award> Get(int id);
		Task<Award> Add(AddAwardDTO dto);
		Task<Award> Update(UpdateAwardDTO dto);
		Task<Award> Delete(int id);
	}
}
