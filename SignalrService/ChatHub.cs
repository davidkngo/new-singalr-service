using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalrService
{
    public class ChatHub : Hub
    {

        public int OnCounter = 0;

        public override Task OnConnectedAsync()
        {
            Clients.All.InvokeAsync("ShowUsersNumber", ++OnCounter);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.All.InvokeAsync("ShowUsersNumber", --OnCounter);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendToAll(ChatMessage message)
        {
            await Clients.All.InvokeAsync("Send", message);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveAsync(Context.ConnectionId, groupName);
        }

        public async Task SendToGroup(string groupName, ChatMessage message)
        {
            await Clients.Group(groupName).InvokeAsync("GroupMessage", message);
        }
    }
}