using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ExternalServices.Chat
{
	public class ChatHub : Hub
	{
		public async Task SendMessageToUser(int senderId, int receiverId, string message)
		{
			await Clients.User(receiverId.ToString()).SendAsync("ReceiveMessage", senderId, message);
		}

		public override async Task OnConnectedAsync()
		{
            var httpContext = Context.GetHttpContext();
            string userId = httpContext.Request.Query["userId"];
			await Groups.AddToGroupAsync(Context.ConnectionId, userId);
			await base.OnConnectedAsync();
		}
	}
}
