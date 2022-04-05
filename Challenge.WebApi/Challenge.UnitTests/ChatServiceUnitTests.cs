using Challenge.Domain.Entities;
using Challenge.Infrastructure;
using Challenge.Infrastructure.Entities;
using Challenge.Infrastructure.Repository;
using Challenge.Infrastructure.WebSocket;
using Challenge.Service.Implementations;
using Challenge.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Challenge.UnitTests
{
    [TestClass]
    public class ChatServiceUnitTests
    {

        [TestMethod]
        public async Task call_correct_services()
        {
            Mock<IChatMessageRepository> chatRepository = new Mock<IChatMessageRepository>();
            Mock<UserManager<ChallengeUser>> userManagerService = new Mock<UserManager<ChallengeUser>>(Mock.Of<IUserStore<ChallengeUser>>(), null, null, null, null, null, null, null, null);
            Mock<IWebSocketManager> webSocketService = new Mock<IWebSocketManager>();
            userManagerService.Setup(x => x.FindByNameAsync("username")).Returns(Task.FromResult(new ChallengeUser { UserName = "test" }));
            chatRepository.Setup(x => x.Add(It.IsAny<ChatMessage>()));
            webSocketService.Setup(x => x.SendMessageToAllAsync(It.IsAny<string>(), It.IsAny<int>()));

            ChatService messageService = new ChatService(chatRepository.Object,userManagerService.Object, webSocketService.Object);
            messageService.Send(new Message() { Chatroom = 1, Content="test", Username="username" });

            chatRepository.Verify(m=> m.Add(It.IsAny<ChatMessage>()), Times.Once);
            userManagerService.Verify(m => m.FindByNameAsync("username"), Times.Once);
            webSocketService.Verify(m => m.SendMessageToAllAsync(It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }
    }
}