using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalrService
{
    public class ChatHub : Hub
    {
        public async Task SendToAll(ChatMessage message)
        {
            await Clients.All.InvokeAsync("Send", message);
        }
    }
}
