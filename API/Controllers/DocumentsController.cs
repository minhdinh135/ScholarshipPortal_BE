using Application.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class DocumentsController : ControllerBase
	{
		private readonly IGenericRepository<Document> _documentRepo;
		private readonly ILogger<DocumentsController> _logger;

		public DocumentsController(IGenericRepository<Document> documentRepo, ILogger<DocumentsController> logger)
		{
			_documentRepo = documentRepo;
			_logger = logger;
		}

		// Get all documents
		[HttpGet]
		public async Task<IActionResult> GetAllDocuments()
		{
			try
			{
				var documents = await _documentRepo.GetAll(x => x.Include(d => d.Application));
				return Ok(documents);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all documents: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		// Get document by ID
		[HttpGet("{id}")]
		public async Task<IActionResult> GetDocumentById(Guid id)
		{
			try
			{
				var document = await _documentRepo.Get(id);
				if (document == null)
				{
					return NotFound("Document not found.");
				}
				return Ok(document);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get document by id {id}: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		// Add new document
		[HttpPost("Add")]
		public async Task<IActionResult> AddDocument([FromBody] DocumentDTO documentDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var newDocument = new Document
				{
					Name = documentDto.Name,
					Description = documentDto.Description,
					Content = documentDto.Content,
					Type = documentDto.Type,
					FilePath = documentDto.FilePath,
					ApplicationId = documentDto.ApplicationId
				};

				var addedDocument = await _documentRepo.Add(newDocument);
				return Ok(addedDocument);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add document: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data to the database.");
			}
		}

		// Update existing document
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateDocument(Guid id, [FromBody] DocumentDTO documentDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var document = await _documentRepo.Get(id);
				if (document == null)
					return NotFound("Document not found.");

				document.Name = documentDto.Name;
				document.Description = documentDto.Description;
				document.Content = documentDto.Content;
				document.Type = documentDto.Type;
				document.FilePath = documentDto.FilePath;
				document.ApplicationId = documentDto.ApplicationId;

				var updatedDocument = await _documentRepo.Update(document);
				return Ok(updatedDocument);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update document: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
			}
		}

		// Delete document by ID
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDocument(Guid id)
		{
			try
			{
				var deletedDocument = await _documentRepo.Delete(id);
				if (deletedDocument == null)
					return NotFound("Document not found.");

				return Ok(deletedDocument);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to delete document: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
			}
		}
	}
}
