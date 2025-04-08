namespace Application.Interfaces.IServices;

public interface IFirebaseNotificationService
{
    Task<string> SendNotification(string topic, string link, string title, string body);
    Task<string> SubscribeToTopic(string token, string topic);
}
