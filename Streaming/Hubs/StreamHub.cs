using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SoftServe.ITAcademy.BackendDubbingProject.Streaming.Core.Hubs
{
    public class StreamHub : Hub
    {
        public async Task SendMessage(string message)
        {
            string answer;

            switch (message)
            {
                case "Start":
                    answer = message;
                    break;
                case "End":
                    answer = message;
                    break;
                case "Resume":
                    answer = message;
                    break;
                case "Pause":
                    answer = message;
                    break;
                default:
                    answer = message;
                    break;
            }

            await Clients.Others.SendAsync("ReceiveMessage", answer);
        }
    }
}