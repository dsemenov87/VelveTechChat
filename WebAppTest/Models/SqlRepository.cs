using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Ninject;
using VelveTechChat.Models.DAL;

namespace VelveTechChat.Models
{
    public sealed class SqlRepository : IRepository, IDisposable
    {
        [Inject]
        public MainDbContext Db { get; set; }

        public IQueryable<ChatMessage> ChatMessages { get { return Db.ChatMessages; } }

        public ChatMessage CreateMessage(string text, string authorId)
        {
            return Db.ChatMessages.Add(new ChatMessage
            {
                Id = Guid.NewGuid(),
                Text = text,
                Created = DateTime.Now,
                AuthorId = new Guid(authorId)
            });
        }

        public void Commit()
        {
            Db.SaveChanges();
        }

        public void Dispose()
        {
            if (Db != null)
            {
                Db.Dispose();
            }

            GC.SuppressFinalize(this);
        }

        ~SqlRepository()
        {
            Dispose();
        }
    }
}