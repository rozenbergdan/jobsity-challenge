using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Infrastructure.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public ChatRoom ChatRoom { get; set; }
        public int ChatRoomId { get; set; }
        public ChallengeUser User { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
    }
}
