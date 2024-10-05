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
		Task<IEnumerable<Document>> GetAll();
		Task<Document> Get(int id);
		Task<Document> Add(AddDocumentDTO dto);
		Task<Document> Update(UpdateDocumentDTO dto);
		Task<Document> Delete(int id);
	}
}
