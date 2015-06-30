using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VelveTechChat.Models
{
    public interface IRepository : IDisposable
    {
        IQueryable<ChatMessage> ChatMessages { get; }

        ChatMessage CreateMessage(string text, string authorId);

        void Commit();
    }
}
