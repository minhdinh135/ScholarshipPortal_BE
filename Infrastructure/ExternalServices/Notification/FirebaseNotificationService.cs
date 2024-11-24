using Application.Interfaces.IServices;
using FirebaseAdmin.Messaging;

namespace Infrastructure.ExternalServices.Notification;

public class FirebaseNotificationService : IFirebaseNotificationService
{
    public async Task<string> SendNotification(string topic, string link, string title, string body)
    {
        var data = new Dictionary<string, string>();
        data.Add("link", link);
        data.Add("topic", topic);
        data.Add("icon", "https://res.cloudinary.com/djiztef3a/image/upload/v1729315324/yncpkzj4unhr7le4fzox.jpg");
        var message = new Message()
        {
            Topic = topic,
            Notification = new FirebaseAdmin.Messaging.Notification
            {
                Title = title,
                Body = body,
            },
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
            if (res.Errors.Count > 0) Console.WriteLine(res.Errors.FirstOrDefault().Reason);
            return res.SuccessCount.ToString();
        }
        catch (FirebaseMessagingException e)
        {
            return e.Message;
        }
    }
}