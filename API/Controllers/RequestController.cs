﻿using Application.Exceptions;
using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Domain.DTOs.Request;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/requests")]
public class RequestController : ControllerBase
{
    private readonly IRequestService _requestService;

    public RequestController(IRequestService requestService)
    {
        _requestService = requestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRequests()
    {
        var requests = await _requestService.GetAllRequests();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all requests successfully", requests));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRequestById(int id)
    {
        try
        {
            var request = await _requestService.GetRequestById(id);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Get request successfully", request));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateRequest(AddRequestDto addRequestDto)
    {
        try
        {
            var createdRequest = await _requestService.CreateRequest(addRequestDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Create request successfully", createdRequest));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRequest(int id, UpdateRequestDto updateRequestDto)
    {
        try
        {
            var updatedRequest = await _requestService.UpdateRequest(id, updateRequestDto);

            return Ok(new ApiResponse(StatusCodes.Status200OK, "Update request successfully", updatedRequest));
        }
        catch (ServiceException e)
        {
            return BadRequest(new ApiResponse(StatusCodes.Status400BadRequest, e.Message));
        }
    }
}