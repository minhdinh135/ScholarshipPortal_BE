using Domain.DTOs.Category;

namespace Domain.DTOs.Common;

public class ApiResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }

    public ApiResponse()
    {
    }

    public ApiResponse(int statusCode, string message, object data)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }
};