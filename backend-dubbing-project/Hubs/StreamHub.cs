using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SoftServe.ITAcademy.BackendDubbingProject.Hubs
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
                    answer = IdValidation(message);
                    break;
            }

            await Clients.Others.SendAsync("ReceiveMessage", answer);
        }

        private static string IdValidation(string id)
        {
            var number = char.Parse(id);

            var idIsValid = char.IsNumber(number);

            if (idIsValid)
                return id;

            throw new ArgumentException();
        }
    }
}