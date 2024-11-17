using FirebaseAdmin.Messaging;
using Application.Interfaces.IServices;
using Domain.DTOs.Notification;
using Domain.DTOs.Common;
using AutoMapper;
using Application.Interfaces.IRepositories;

namespace Infrastructure.ExternalServices.Notification;
public class NotificationsService : INotificationService
{
    private readonly IMapper _mapper;
    private readonly IGenericRepository<Domain.Entities.Notification> _notificationRepository;

    public NotificationsService(IMapper mapper, 
            IGenericRepository<Domain.Entities.Notification> notificationService)
    {
        _mapper = mapper;
        _notificationRepository = notificationService;
    }

    public async Task<NotificationDTO> Add(NotificationAddDTO dto)
    {
        var noti = _mapper.Map<Domain.Entities.Notification>(dto);

        var createdNoti = await _notificationRepository.Add(noti);

        return _mapper.Map<NotificationDTO>(createdNoti);
    }

    public async Task<NotificationDTO> DeleteById(int id)
    {
        var deletedNoti = await _notificationRepository.DeleteById(id);

        return _mapper.Map<NotificationDTO>(deletedNoti);
    }

    public async Task<IEnumerable<NotificationDTO>> GetAll()
    {
        var all = await _notificationRepository.GetAll();

        return _mapper.Map<IEnumerable<NotificationDTO>>(all);
    }

    public async Task<PaginatedList<NotificationDTO>> GetAll(int pageIndex, int pageSize, string sortBy, string sortOrder)
    {
        var notis = await _notificationRepository.GetPaginatedList(pageIndex, pageSize, sortBy, sortOrder);

        return _mapper.Map<PaginatedList<NotificationDTO>>(notis);
    }

    public async Task<NotificationDTO> GetById(int id)
    {
        var noti = await _notificationRepository.GetById(id);

        return _mapper.Map<NotificationDTO>(noti);
    }

    public async Task<string> SendNotification(string topic, string link, string title, string body)
    {
        var data = new Dictionary<string, string>();
        data.Add("link", link);
        data.Add("topic", topic);
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
        var noti = await _notificationRepository.Add(new Domain.Entities.Notification
        {
             ReceiverId = Int32.Parse(topic),
             //Link = link,
             Message = body,
             IsRead = false,
             SentDate = DateTime.Now,
             //Body = body,
        //     Icon = "",
        //     Time = DateTime.Now,
        //     Status = "UNREAD"
        });
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

    public async Task<NotificationDTO> Update(int id, NotificationUpdateDTO dto)
    {
        var existingNoti = await _notificationRepository.GetById(id);

        _mapper.Map(dto, existingNoti);

        var updatedMajor = await _notificationRepository.Update(existingNoti);

        return _mapper.Map<NotificationDTO>(updatedMajor);
    }
}
