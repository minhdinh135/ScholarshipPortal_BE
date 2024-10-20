using Domain.DTOs.Common;
using Domain.DTOs.Notification;

namespace Application.Interfaces.IServices;

public interface INotificationService
{
    Task<IEnumerable<NotificationDTO>> GetAll();
    Task<PaginatedList<NotificationDTO>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder);
    Task<NotificationDTO> GetById(int id);
    Task<NotificationDTO> Add(NotificationAddDTO dto);
    Task<NotificationDTO> Update(int id, NotificationUpdateDTO dto);
    Task<NotificationDTO> DeleteById(int id);

    Task<string> SendNotification(string topic, string link, string title, string body);
    Task<string> SubscribeToTopic(string token, string topic);
}
