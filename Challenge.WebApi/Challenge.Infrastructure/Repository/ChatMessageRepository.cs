using Challenge.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Repository
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly ChallengeDbContext db;
        public ChatMessageRepository(ChallengeDbContext db)
        {
            this.db = db;
        }

        public void Add(ChatMessage item)
        {
            using (this.db)
            {
                this.db.ChatMessage.Add(item);
                this.db.SaveChanges();
            }
        }

        public ChatMessage Find(object id)
        {
            using (this.db)
            {
                return this.db.ChatMessage.Find(id);
            }
        }

        public IEnumerable<ChatMessage> List(int chatroom, int size= 50)
        {
            using (this.db)
            {
                return this.db.ChatMessage.Include(x=>x.User).Where(x=> x.ChatRoomId == chatroom).OrderByDescending(x=> x.Created).Take(size).ToList();
            }
        }
    }
}
