using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs;
using Domain.DTOs.Document;
using Domain.Entities;

namespace Application.Services
{
	public class DocumentService : IDocumentService
	{
		private readonly IGenericRepository<Document> _repository;
		private readonly IMapper _mapper;

		public DocumentService(IGenericRepository<Document> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<Document> Add(AddDocumentDTO dto)
		{
			var entity = _mapper.Map<Document>(dto);
			await _repository.Add(entity);
			return entity;
		}

		public async Task<Document> Delete(int id)
		{
			var entity = await _repository.GetById(id);
			if (entity == null) return null;
			await _repository.DeleteById(id);
			return entity;
		}

		public async Task<Document> Get(int id)
		{
			return await _repository.GetById(id);
		}

		public async Task<IEnumerable<Document>> GetAll()
		{
			return await _repository.GetAll();
		}

		public async Task<Document> Update(UpdateDocumentDTO dto)
		{
			var university = await _repository.GetAll();
			var exist = university.Any(u => u.Id == dto.Id);
			if (!exist) throw new Exception("Document not found.");
			var entity = _mapper.Map<Document>(dto);
			await _repository.Update(entity);
			return entity;
		}
	}
}
