using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace VelveTechChat.Models.DAL
{
    public class MainDbContext : DbContext 
    {
        public MainDbContext() : base("Library") { }

        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}
