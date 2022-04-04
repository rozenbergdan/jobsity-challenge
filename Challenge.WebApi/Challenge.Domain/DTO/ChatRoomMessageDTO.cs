﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Domain.DTO
{
    public class ChatRoomMessageDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
    }
}
