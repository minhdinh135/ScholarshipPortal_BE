using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Notification
{
	public class FeedbackNotificationRequest
	{
		public int ProviderId { get; set; }
		public string ServiceName { get; set; }
	}
}
