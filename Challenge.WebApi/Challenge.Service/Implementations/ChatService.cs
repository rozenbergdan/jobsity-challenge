using Challenge.Domain.Entities;
using Challenge.Infrastructure.Entities;
using Challenge.Infrastructure.Repository;
using Challenge.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Service.Implementations
{
    public class ChatService : IMessage
    {
        private readonly IChatMessageRepository repository;
        public UserManager<ChallengeUser> UserManager { get; }

        public ChatService(IChatMessageRepository repository, UserManager<ChallengeUser> userManager)
        {
            this.repository = repository;
            UserManager = userManager;
        }



        public void Send(Message message)
        {
            var user = UserManager.FindByNameAsync(message.Username).Result;
            this.repository.Add(new ChatMessage
            {
                Content = message.Content,
                ChatRoomId = message.Chatroom,
                UserId = user.Id,
                Created = message.Date
            });
        }
    }
}
