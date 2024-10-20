using FirebaseAdmin.Messaging;
using Application.Interfaces.IServices;
using Domain.DTOs.Notification;
using Domain.DTOs.Common;

namespace Infrastructure.ExternalServices.Notification;
public class NotificationsService : INotificationService
{
    public Task<NotificationDTO> Add(NotificationAddDTO dto)
    {
        throw new NotImplementedException();
    }

    public Task<NotificationDTO> DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<NotificationDTO>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<PaginatedList<NotificationDTO>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder)
    {
        throw new NotImplementedException();
    }

    public Task<NotificationDTO> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<string> SendNotification(string topic, string link, string title, string body)
    {
        var data = new Dictionary<string, string>();
        data.Add("link", link);
        data.Add("icon", "https://res.cloudinary.com/djiztef3a/image/upload/v1729315324/yncpkzj4unhr7le4fzox.jpg");
        var message = new Message()
        {
            Notification = new FirebaseAdmin.Messaging.Notification
            {
                Title = title,
                Body = body,
                //ImageUrl = "https://res.cloudinary.com/djiztef3a/image/upload/v1729315324/yncpkzj4unhr7le4fzox.jpg"
            },
            Topic = topic,
            Data = data
        };

        string response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
        return response;
    }

    public async Task<string> SubscribeToTopic(string token, string topic)
    {
        try
        {
            // Subscribe the token to the topic (which could be the user's unique ID)
            var res = await FirebaseMessaging.DefaultInstance.SubscribeToTopicAsync(new List<string> { token }, topic);
            if(res.Errors.Count > 0) Console.WriteLine(res.Errors.FirstOrDefault().Reason);
            return res.SuccessCount.ToString();
        }
        catch (FirebaseMessagingException e)
        {
            return e.Message;
        }
    }

    public Task<NotificationDTO> Update(int id, NotificationUpdateDTO dto)
    {
        throw new NotImplementedException();
    }
}
