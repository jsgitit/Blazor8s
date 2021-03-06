using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Blazor8s.Server.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message); 
        }

        public async Task EchoMessage(string user, string message)
        {
            await Clients.Caller.SendAsync("ReceiveMessage", user, message); 
        }
    } 
}