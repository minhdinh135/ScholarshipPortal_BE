using Application.Interfaces.IServices;
using Domain.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/notification")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpPost("send-notification")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
    {
        var response = await _notificationService.SendNotification(request.Topic, request.Title, request.Body);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Send notification successfully", response));
    }

    [HttpPost("subscribe-to-topic")]
    public async Task<IActionResult> SubscribeToTopic([FromBody] TopicRequest request)
    {
        var response = await _notificationService.SubscribeToTopic(request.Token, request.Topic);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Send notification successfully", response));
    }
}

public class NotificationRequest
{
    public string Topic { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}


public class TopicRequest
{
    public string Token { get; set; }
    public string Topic { get; set; }
}
