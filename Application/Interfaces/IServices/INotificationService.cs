namespace Application.Interfaces.IServices;

public interface INotificationService
{
    Task<string> SendNotification(string topic, string title, string body);
    Task<string> SubscribeToTopic(string token, string topic);
}
