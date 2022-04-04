using Challenge.Infrastructure.Entities;

namespace Challenge.Infrastructure.Repository
{
    public interface IChatMessageRepository
    {
        void Add(ChatMessage item);
        IEnumerable<ChatMessage> List(int chatroom, int size);

        ChatMessage Find(object id);
    }
}