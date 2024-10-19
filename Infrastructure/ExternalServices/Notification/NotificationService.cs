using FirebaseAdmin.Messaging;
using Application.Interfaces.IServices;

namespace Infrastructure.ExternalServices.Notification;
public class NotificationsService : INotificationService
{
    public async Task<string> SendNotification(string topic, string title, string body)
    {
        var message = new Message()
        {
            Notification = new FirebaseAdmin.Messaging.Notification
            {
                Title = title,
                Body = body,
                ImageUrl = "https://res.cloudinary.com/djiztef3a/image/upload/v1729315324/yncpkzj4unhr7le4fzox.jpg"
            },
            Topic = topic
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
            return res.SuccessCount.ToString();
        }
        catch (FirebaseMessagingException e)
        {
            return e.Message;
        }
    }
}
