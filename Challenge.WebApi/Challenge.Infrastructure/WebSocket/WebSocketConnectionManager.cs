using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;

namespace Challenge.Infrastructure
{
    public class WebSocketConnectionManager
    {
        public WebSocketConnectionManager()
        {

        }

        private ConcurrentDictionary<int,ConcurrentDictionary<string, WebSocket>> _sockets = new ConcurrentDictionary<int, ConcurrentDictionary<string, WebSocket>>();

        public WebSocket GetSocketById(string id,int roomid)
        {
            return _sockets[roomid].FirstOrDefault(p => p.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll(int roomid)
        {
            return _sockets[roomid];
        }

        public string GetId(WebSocket socket,int roomid)
        {
            return _sockets[roomid].FirstOrDefault(p => p.Value == socket).Key;
        }

        public void AddSocket(WebSocket socket, int roomid)
        {
            if (!_sockets.ContainsKey(roomid))
                _sockets.TryAdd(roomid, new ConcurrentDictionary<string, WebSocket>());

            _sockets[roomid].TryAdd(CreateConnectionId(), socket);
        }

        public async Task RemoveSocket(WebSocket socket,int roomid)
        {
            var id = GetId(socket,roomid);
            await RemoveSocket(id,roomid);
        }

        public async Task RemoveSocket(string id,int roomid)
        {
            WebSocket socket;
            _sockets[roomid].TryRemove(id, out socket);

            await socket.CloseAsync(closeStatus: System.Net.WebSockets.WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the System.Net.WebSockets.WebSocketManager",
                                    cancellationToken: CancellationToken.None);
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            if (socket.State != System.Net.WebSockets.WebSocketState.Open)
                return;

            var msgByteArray = Encoding.UTF8.GetBytes(message);

            await socket.SendAsync(buffer: new ArraySegment<byte>(array: msgByteArray,
                                                                  offset: 0,
                                                                  count: msgByteArray.Length),
                                   messageType: System.Net.WebSockets.WebSocketMessageType.Text,
                                   endOfMessage: true,
                                   cancellationToken: CancellationToken.None);
        }

        public async Task SendMessageAsync(string socketId, string message, int roomid)
        {
            await SendMessageAsync(GetSocketById(socketId,roomid), message);
        }

        public async Task SendMessageToAllAsync(string message,int roomid)
        {
            foreach (var pair in GetAll(roomid))
            {
                if (pair.Value.State == System.Net.WebSockets.WebSocketState.Open)
                    await SendMessageAsync(pair.Value, message);
            }
        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
