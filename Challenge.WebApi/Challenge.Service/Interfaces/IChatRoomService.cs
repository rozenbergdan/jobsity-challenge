using Challenge.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Service.Interfaces
{
    public interface IChatRoomService
    {
        IEnumerable<ChatRoomMessageDTO> GetLasts(int chatroom, int size);
    }
}
