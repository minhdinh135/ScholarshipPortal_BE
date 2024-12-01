using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Notification
{
	public class RejectNotificationRequest
	{
		public string Topic { get; set; } 
		public string Link { get; set; } 
		public string Title { get; set; } 
		public string Body { get; set; }
	}

}
