using Challenge.Domain.DTO;
using Challenge.Infrastructure.Entities;
using Challenge.Infrastructure.Repository;
using Challenge.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Service.Implementations
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IChatMessageRepository chatmessageRepository;

        public ChatRoomService(IChatMessageRepository chatmessageRepository)
        {
            this.chatmessageRepository = chatmessageRepository;
        }

        public IEnumerable<ChatRoomMessageDTO> GetLasts(int chatroom, int size)
        {
            var chats = chatmessageRepository.List(chatroom, size);
            return chats.Select(x => new ChatRoomMessageDTO { 
                Id = x.Id,
                Content = x.Content,
                Created = x.Created,
                UserName = x.User.UserName,
            });
        }
    }
}
