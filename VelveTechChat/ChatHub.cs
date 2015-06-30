using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Ninject;

using VelveTechChat.Models;

namespace VelveTechChat
{
    public class ChatHub : Hub
    {
        [Inject]
        public IRepository Repository { get; set; }
        
        public void Send(string message, string authorId)
        {
            var item = Repository.CreateMessage(message, authorId);
            Repository.Commit();

            Clients.All.addNewMessageToPage(item.Id, message, authorId);
        }
    }
}