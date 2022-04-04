using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Domain.DTO
{
    public class MessageDTO
    {
        public MessageDTO(){ }
        public MessageDTO(string message)
        {
            Message = message;
        }
        public string Message { get; set; } 
        

    }
}
