using Application.Interfaces.IServices;
using Domain.DTOs.Category;
using Domain.DTOs.Common;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var allCategories = await _categoryService.GetAllCategories();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all categories successfully", allCategories));
    }

    [HttpGet("paginated")]
    public async Task<IActionResult> GetCategories([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10,
        [FromQuery] string sortBy = "Id", [FromQuery] string sortOrder = "asc")
    {
        var categories = await _categoryService.GetCategories(pageIndex, pageSize, sortBy, sortOrder);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get categories successfully", categories));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] int id)
    {
        var category = await _categoryService.GetCategoryById(id);

        if (category == null)
            return NotFound(new ApiResponse(StatusCodes.Status404NotFound, "Category not found", null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get category successfully", category));
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var createdCategory = await _categoryService.CreateCategory(createCategoryRequest);

        if (createdCategory == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Create category failed", null));

        return Created("/api/categories/" + createdCategory.Id,
            new ApiResponse(StatusCodes.Status201Created, "Create category successfully", createdCategory));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] UpdateCategoryRequest updateCategoryRequest)
    {
        var updatedCategory = await _categoryService.UpdateCategory(id, updateCategoryRequest);

        if (updatedCategory == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Update category failed", null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Update category successfully", updatedCategory));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory([FromRoute] int id)
    {
        var deletedCategory = await _categoryService.DeleteCategoryById(id);

        if (deletedCategory == null)
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, "Delete category failed", null));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Delete category successfully", deletedCategory));
    }
}