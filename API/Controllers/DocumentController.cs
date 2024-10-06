using Application.Interfaces.IServices;
using Domain.DTOs;
using Domain.DTOs.Document;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers
{
	[ApiController]
	[Route("api/documents")]
	public class DocumentController : ControllerBase
	{
		private readonly IDocumentService _documentService;
		private readonly ILogger<DocumentController> _logger;

		public DocumentController(IDocumentService documentService, ILogger<DocumentController> logger)
		{
			_documentService = documentService;
			_logger = logger;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllDocuments()
		{
			try
			{
				var documents = await _documentService.GetAll();
				return Ok(documents);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get all documents: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetDocumentById(int id)
		{
			try
			{
				var document = await _documentService.Get(id);
				return Ok(document);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to get document by id {id}: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
			}
		}

		[HttpPost("Add")]
		public async Task<IActionResult> AddDocument([FromBody] AddDocumentDTO documentDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var addedDocument = await _documentService.Add(documentDto);
				return Ok(addedDocument);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to add document: {ex.Message}");
				return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data to the database.");
			}
		}

		[HttpPut]
		public async Task<IActionResult> UpdateDocument([FromBody] UpdateDocumentDTO documentDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			try
			{
				var updatedDocument = await _documentService.Update(documentDto);
				return Ok(updatedDocument);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed to update document: {ex.Message}");
				return BadRequest(new { Message = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDocument(int id)
		{
			try
			{
				var deletedDocument = await _documentService.Delete(id);

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
