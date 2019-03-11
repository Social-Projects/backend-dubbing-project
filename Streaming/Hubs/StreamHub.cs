using System;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SoftServe.ITAcademy.BackendDubbingProject.Streaming.Core.Hubs
{
    internal class StreamHub : Hub
    {
        private static int _count;
        private string _adminId;

        public override async Task OnConnectedAsync()
        {
            _count++;

            await base.OnConnectedAsync();
            await Clients.User(_adminId).SendAsync("updateCount", (_count - 1).ToString());
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            _count--;

            await base.OnDisconnectedAsync(exception);
            await Clients.User(_adminId).SendAsync("updateCount", (_count - 1).ToString());
        }

        public async Task SendMessage(string message)
        {
            var answer = message;

            if (message == "start")
            {
                _adminId = Context.ConnectionId;
            }

            await Clients.Others.SendAsync("ReceiveMessage", answer);
        }
    }
}