using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int Chatroom { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public DateTimeOffset Date { get; set; }

    }
}
