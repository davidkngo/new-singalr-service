using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalrService
{
    public class ChatHub : Hub
    {

        public int OnCounter = 0;

        public override async Task OnConnectedAsync()
        {
            await Clients.All.InvokeAsync("ShowUsersOnline", ++OnCounter);
            await base.OnConnectedAsync();
        }

        public async Task SendToAll(ChatMessage message)
        {
            await Clients.All.InvokeAsync("SendToAll", message);
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
            await Clients.Group(groupName).InvokeAsync("SendToGroup", message);
        }
    }
}
