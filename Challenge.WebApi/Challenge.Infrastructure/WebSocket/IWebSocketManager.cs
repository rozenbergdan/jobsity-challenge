using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;

namespace Challenge.Infrastructure.WebSocket
{
    public interface IWebSocketManager
    {
        System.Net.WebSockets.WebSocket GetSocketById(string id, int roomid);

        ConcurrentDictionary<string, System.Net.WebSockets.WebSocket> GetAll(int roomid);

        string GetId(System.Net.WebSockets.WebSocket socket, int roomid);

        void AddSocket(System.Net.WebSockets.WebSocket socket, int roomid);

        Task RemoveSocket(System.Net.WebSockets.WebSocket socket, int roomid);

        Task RemoveSocket(string id, int roomid);

        Task SendMessageAsync(System.Net.WebSockets.WebSocket socket, string message);

        Task SendMessageAsync(string socketId, string message, int roomid);

        Task SendMessageToAllAsync(string message, int roomid);
        
    }
}
