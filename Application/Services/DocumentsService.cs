using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
	public class DocumentsService : IDocumentsService
	{
		private readonly IGenericRepository<Document> _repository;
		private readonly IMapper _mapper;

		public DocumentsService(IGenericRepository<Document> repository, IMapper mapper)
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
			var entity = await _repository.Get(id);
			if (entity == null) return null;
			await _repository.Delete(id);
			return entity;
		}

		public async Task<Document> Get(int id)
		{
			return await _repository.Get(id);
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
