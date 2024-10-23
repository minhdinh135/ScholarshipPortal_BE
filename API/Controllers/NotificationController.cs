using Application.Interfaces.IServices;
using AutoMapper;
using Domain.Constants;
using Domain.DTOs.Account;
using Domain.DTOs.Common;
using Domain.DTOs.Notification;
using Microsoft.AspNetCore.Mvc;

namespace SSAP.API.Controllers;

[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public NotificationController(INotificationService notificationService, IMapper mapper,
        IAccountService accountService)
    {
        _notificationService = notificationService;
        _mapper = mapper;
        _accountService = accountService;
    }

    [HttpGet("get-all-by-id/{id}")]
    public async Task<IActionResult> GetAll(int id, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10,
        [FromQuery] string sortBy = "SentDate", [FromQuery] string sortOrder = "desc")
    {
        var response = await _notificationService.GetAll(pageIndex, pageSize, sortBy, sortOrder);
        response.Items = response.Items.Where(x => x.ReceiverId == id).ToList();

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all notification successfully", response));
    }

    [HttpPost("send-notification")]
    public async Task<IActionResult> SendNotification([FromBody] NotificationRequest request)
    {
        var response = await _notificationService.SendNotification(request.Topic, request.Link, request.Title, request.Body);
        

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Send notification successfully", response));
    }

    [HttpPost("notify-new-user/{userId}")]
    public async Task<IActionResult> NotifyNewUser( int userId)
    {
        try
        {
            var user = await _accountService.GetAccount(userId);
            //send notification
            await _notificationService.SendNotification(user.Id.ToString(), "/account-info", "Welcome", "Update your profile now.");
            await _notificationService.SendNotification(user.Id.ToString(), "/scholarship-program", "Welcome", "Check out our scholarship program.");
            //get all admins
            var admins = await _accountService.GetAll();
            admins = admins.Where(x => x.RoleName == RoleEnum.ADMIN).ToList();

            foreach (var admin in admins)
            {
                await _notificationService.SendNotification(admin.Id.ToString(), "/admin/accountsmanagement", "New User", $"{user.Username} has registered.");
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPut("read/{id}")]
    public async Task<IActionResult> ReadNotification(int id)
    {
        var response = await _notificationService.GetById(id);
        response.IsRead = true;

        response = await _notificationService.Update(id, _mapper.Map<NotificationUpdateDTO>(response));

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Send notification successfully", response));
    }

    [HttpPost("subscribe-to-topic")]
    public async Task<IActionResult> SubscribeToTopic([FromBody] TopicRequest request)
    {
        var response = await _notificationService.SubscribeToTopic(request.Token, request.Topic);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Send notification successfully", response));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _notificationService.DeleteById(id);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Send notification successfully", response));
    }
}

public class NotificationRequest
{
    public string Topic { get; set; }
    public string Link { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}


public class TopicRequest
{
    public string Token { get; set; }
    public string Topic { get; set; }
}
