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
    private readonly IScholarshipProgramService _scholarshipProgramService;
    private readonly IServiceService _serviceService;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly ISubscriptionService _subscriptionService;

    public NotificationController(INotificationService notificationService, IMapper mapper,
        IAccountService accountService,
        IScholarshipProgramService scholarshipProgramService,
        IEmailService emailService,
        IServiceService serviceService,
        ISubscriptionService subscriptionService)
    {
        _notificationService = notificationService;
        _mapper = mapper;
        _accountService = accountService;
        _scholarshipProgramService = scholarshipProgramService;
        _emailService = emailService;
        _serviceService = serviceService;
        _subscriptionService = subscriptionService;
    }

    [HttpGet("get-all-by-id/{id}")]
    public async Task<IActionResult> GetAll(int id)
    {
        var response = await _notificationService.GetAll();
        response = response.Where(x => x.ReceiverId == id).ToList().OrderByDescending(x => x.CreatedAt);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Get all notification successfully", response));
    }

    [HttpPost("send-notification-and-email")]
    public async Task<IActionResult> SendNotificationAndEmail([FromBody] NotificationRequest request)
    {
        var response = await _notificationService.SendNotification(request.Topic, request.Link, request.Title, request.Body);
        AccountDto? user = null;
        if(int.TryParse(request.Topic, out var id)){
           user = await _accountService.GetAccount(id);
        }
        if (user != null) await _emailService.SendEmailAsync(user.Email, request.Title, request.Body);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Send notification successfully", response));
    }

    [HttpPost("send-extend-reason")]
    public async Task<IActionResult> SendExtendReason([FromBody] NotificationRequest request)
    {
        var response = await _notificationService.SendNotification(request.Topic, request.Link, "Your application need more document",
            "An email has been sent to your email. Kindly check your email for more information.");
        AccountDto? user = null;
        if(int.TryParse(request.Topic, out var id)){
           user = await _accountService.GetAccount(id);
        }
        if (user != null) await _emailService.SendEmailAsync(user.Email, request.Title, request.Body);

        return Ok(new ApiResponse(StatusCodes.Status200OK, "Send notification successfully", response));
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
            if (user != null) await _emailService.SendEmailAsync(user.Email, "Welcome", "Welcome to SSAP! You can check out our scholarship program.");

            //get all admins
            var admins = await _accountService.GetAll();
            admins = admins.Where(x => x.RoleName == RoleEnum.Admin.ToString()).ToList();

            foreach (var admin in admins)
            {
                await _notificationService.SendNotification(admin.Id.ToString(), "/admin/accountsmanagement", "New User", $"{user.Username} has registered.");
                await _emailService.SendEmailAsync(admin.Email, "New User", $"{user.Username} has registered.");
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("notify-funder-new-applicant")]
    public async Task<IActionResult> NotifyFunderNewApplicant( [FromQuery]int scholarshipId, [FromQuery] int applicantId)
    {
        try
        {
            var scholarship = await _scholarshipProgramService.GetScholarshipProgramById(scholarshipId);
            var applicant = await _accountService.GetAccount(applicantId);
            var funder = await _accountService.GetAccount(scholarship.FunderId.Value);
            
            //send notification to funder
            await _notificationService.SendNotification(scholarship.FunderId.ToString(), "/", "", 
                $"{applicant.Username} has applied to scholarship {scholarship.Name}.");
            await _emailService.SendEmailAsync(funder.Email, $"New Applicant to your {scholarship.Name} scholarship", 
                $"{applicant.Username} has applied to scholarship {scholarship.Name}.");

            //send notification to applicant
            await _notificationService.SendNotification(applicant.Id.ToString(), "/", "", 
                $"You have applied to scholarship {scholarship.Name}.");
            await _emailService.SendEmailAsync(applicant.Email, $"Apply to {scholarship.Name} successfully", 
                $"You have applied to scholarship {scholarship.Name}.");

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("notify-provider-new-request")]
    public async Task<IActionResult> NotifyProviderNewRequest( [FromQuery]int serviceId, [FromQuery] int applicantId)
    {
        try
        {
            var service = await _serviceService.GetServiceById(serviceId);
            var applicant = await _accountService.GetAccount(applicantId);
            var provider = await _accountService.GetAccount(service.ProviderId.Value);
            
            //send notification to provider
            await _notificationService.SendNotification(service.ProviderId.ToString(), "/", "", 
                $"{applicant.Username} has requested to {service.Name}.");
            await _emailService.SendEmailAsync(provider.Email, $"New request to your service {service.Name}", 
                $"{applicant.Username} has requested to {service.Name}.");

            //send notification to applicant
            await _notificationService.SendNotification(applicant.Id.ToString(), "/", "", 
                $"You have requested to service {service.Name}.");
            await _emailService.SendEmailAsync(applicant.Email, $"Request to {service.Name} successfully", 
                $"You have requested to service {service.Name}.");

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

	[HttpPost("notify-applicant-service-comment")]
	public async Task<IActionResult> NotifyApplicantServiceComment([FromQuery] int serviceId, [FromQuery] int applicantId)
	{
		try
		{
			var service = await _serviceService.GetServiceById(serviceId);
			var applicant = await _accountService.GetAccount(applicantId);

			if (service == null || applicant == null)
				return BadRequest(new { Message = "Invalid service, provider, or applicant information." });

			var notificationMessage = $"Provider has commented to your Service's request {service.Name}.";

			await _notificationService.SendNotification(applicant.Id.ToString(), $"/services/{serviceId}", "Service Comment", notificationMessage);

			await _emailService.SendEmailAsync(applicant.Email, "New Comment on Your Service Request", notificationMessage);

			return Ok(new ApiResponse(StatusCodes.Status200OK, "Notification sent successfully", null));
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

	[HttpPost("notify-subscription-purchase")]
	public async Task<IActionResult> NotifySubscriptionPurchase([FromQuery] int subscriptionId, [FromQuery] int userId)
	{
		try
		{
			var subscription = await _subscriptionService.GetSubscriptionById(subscriptionId);
			var user = await _accountService.GetAccount(userId);

			if (subscription == null || user == null)
				return BadRequest(new { Message = "Invalid subscription or user information." });

			var notificationMessage = $"You have successfully purchased the subscription package '{subscription.Name}'.";

			await _notificationService.SendNotification(user.Id.ToString(), "/my-subscriptions", "Subscription Purchased", notificationMessage);

			await _emailService.SendEmailAsync(user.Email, "Subscription Purchase Confirmation",
				$"Dear {user.Username},\n\n{notificationMessage}\n\nThank you for your purchase!");

			return Ok(new ApiResponse(StatusCodes.Status200OK, "Notification sent successfully", null));
		}
		catch (Exception ex)
		{
			return BadRequest(new { Message = ex.Message });
		}
	}

	[HttpPost("send-notification-and-email-reject")]
	public async Task<IActionResult> SendNotificationAndEmailReject([FromBody] RejectNotificationRequest request)
	{
		var response = await _notificationService.SendNotification(request.Topic, request.Link, request.Title, request.Body);

		AccountDto? user = null;
		if (int.TryParse(request.Topic, out var id))
		{
			user = await _accountService.GetAccount(id);
		}

		if (user != null)
		{
			await _emailService.SendEmailAsync(user.Email, request.Title, request.Body);
		}

		return Ok(new ApiResponse(StatusCodes.Status200OK, "Send rejection notification and email successfully", response));
	}

	[HttpPost("account-active/{userId}")]
	public async Task<IActionResult> AccountActive(int userId)
	{
		try
		{
			var user = await _accountService.GetAccount(userId);
			if (user == null)
			{
				return BadRequest(new { Message = "User not found." });
			}

			var notificationTitle = "Account Approved";
			var notificationMessage = "Your account has been approved, please log in again to use more features. Thank you.";
			var notificationLink = "/login";

			await _notificationService.SendNotification(user.Id.ToString(), notificationLink, notificationTitle, notificationMessage);

			await _emailService.SendEmailAsync(user.Email, notificationTitle, notificationMessage);

			return Ok(new ApiResponse(StatusCodes.Status200OK, "Account activation notification sent successfully", null));
		}
		catch (Exception ex)
		{
			return BadRequest(new { Message = ex.Message });
		}
	}


}
