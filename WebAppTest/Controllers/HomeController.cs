using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

using VelveTechChat.Models;

namespace VelveTechChat.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public IRepository Repository { get; set; }
        
        public ActionResult Index(string id)
        {
            var model = new Dictionary<string, object>();
            var chatMessages = GetChatMessages(id);

            model["ChatMessages"] = chatMessages;
            model["Author"] = GetAuthCookie().Value;
            
            var firstMessage = chatMessages.FirstOrDefault();
            model["FromMessageId"] = firstMessage != null
                ? firstMessage.Id.ToString()
                : String.Empty;
            
            return View(model);
        }

        private HttpCookie GetAuthCookie()
        {
            var authCookie = Request.Cookies["__CHAT_AUTH"];
            if (authCookie == null)
            {
                Response.SetCookie(authCookie = new HttpCookie("__CHAT_AUTH")
                {
                    Value = Guid.NewGuid().ToString(),
                    Expires = DateTime.Now.AddDays(365)
                });
            }

            return authCookie;
        }

        private IEnumerable<ChatMessage> GetChatMessages(string fromMessageId)
        {
            var fromMessage = Repository.ChatMessages.FirstOrDefault(i => i.Id.ToString() == fromMessageId);

            var query = (fromMessage == null) 
                ? Repository.ChatMessages
                : Repository.ChatMessages.Where(i => i.Created < fromMessage.Created);
                
            var prevMessages = query
                .OrderByDescending(i => i.Created)
                .Take(10)
                .ToList();

            prevMessages.Reverse();

            if (fromMessage != null)
            {
                return prevMessages.Concat(Repository.ChatMessages
                    .Where(i => i.Created >= fromMessage.Created)
                    .ToList()
                );
            }

            return prevMessages;
        }
    }
}