﻿namespace Domain.DTOs.Notification;

public class NotificationRequest
{
    public string Topic { get; set; }
    public string Link { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}