using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.Document;

namespace Application.Interfaces.IServices
{
	public interface IDocumentService
	{
		Task<IEnumerable<ApplicationDocument>> GetAll();
		Task<ApplicationDocument> Get(int id);
		Task<ApplicationDocument> Add(AddDocumentDTO dto);
		Task<ApplicationDocument> Update(UpdateDocumentDTO dto);
		Task<ApplicationDocument> Delete(int id);
	}
}
