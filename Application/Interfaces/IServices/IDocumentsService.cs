using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.IServices
{
	public interface IDocumentsService
	{
		Task<IEnumerable<Document>> GetAll();
		Task<Document> Get(int id);
		Task<Document> Add(AddDocumentDTO dto);
		Task<Document> Update(UpdateDocumentDTO dto);
		Task<Document> Delete(int id);
	}
}
