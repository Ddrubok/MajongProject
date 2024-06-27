using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GameServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("한국어 알파벳 마작 게임 서버 시작");

            GameServer server = new GameServer();
            int port = 8888; // 원하는 포트 번호로 변경 가능

            try
            {
                await server.StartServer(port);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"서버 실행 중 오류 발생: {ex.Message}");
            }
        }
    }

    public class GameRoom
    {
        public string RoomId { get; set; }
        public TcpClient Owner { get; set; }
        public List<TcpClient> Players { get; set; } = new List<TcpClient>();
        public GameState GameState { get; set; } = new GameState();
    }

    public class GameServer
    {
        private TcpListener tcpListener;
        private Dictionary<string, GameRoom> gameRooms = new Dictionary<string, GameRoom>();

        public async Task StartServer(int port)
        {
            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            Console.WriteLine($"Server started on port {port}");

            while (true)
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    await SendWelcomeMessage(stream);

                    byte[] buffer = new byte[1024];
                    while (true)
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead == 0) break;

                        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        await ProcessMessageAsync(message, client);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
            }
            finally
            {
                RemoveClientFromRooms(client);
                client.Close();
            }
        }

        private async Task ProcessMessageAsync(string message, TcpClient sender)
        {
            GameMessage gameMessage = JsonConvert.DeserializeObject<GameMessage>(message);

            switch (gameMessage.Type)
            {
                case "CreateRoom":
                    await CreateRoom(sender);
                    break;
                case "JoinRoom":
                    await JoinRoom(sender, gameMessage.Data.ToString());
                    break;
                case "DrawTile":
                    // 타일 드로우 로직
                    break;
                case "DiscardTile":
                    // 타일 버리기 로직
                    break;
                case "CompleteWord":
                    // 단어 완성 확인 및 점수 계산 로직
                    break;
                    // 기타 메시지 처리...
            }
        }

        private async Task CreateRoom(TcpClient owner)
        {
            string roomId = Guid.NewGuid().ToString();
            GameRoom newRoom = new GameRoom { RoomId = roomId, Owner = owner };
            newRoom.Players.Add(owner);
            gameRooms[roomId] = newRoom;

            await SendMessageToClient(owner, new GameMessage { Type = "RoomCreated", Data = roomId });
        }

        private async Task JoinRoom(TcpClient player, string roomId)
        {
            if (gameRooms.TryGetValue(roomId, out GameRoom room))
            {
                if (room.Players.Count < 4)
                {
                    room.Players.Add(player);
                    await SendMessageToClient(player, new GameMessage { Type = "JoinedRoom", Data = roomId });
                    await BroadcastToRoom(room, new GameMessage { Type = "PlayerJoined", Data = room.Players.Count });

                    if (room.Players.Count == 4)
                    {
                        await StartGame(room);
                    }
                }
                else
                {
                    await SendMessageToClient(player, new GameMessage { Type = "RoomFull" });
                }
            }
            else
            {
                await SendMessageToClient(player, new GameMessage { Type = "RoomNotFound" });
            }
        }

        private async Task StartGame(GameRoom room)
        {
            // 게임 시작 로직 구현
            await BroadcastToRoom(room, new GameMessage { Type = "GameStarted" });
        }

        private async Task SendMessageToClient(TcpClient client, GameMessage message)
        {
            string json = JsonConvert.SerializeObject(message);
            byte[] messageBytes = Encoding.UTF8.GetBytes(json);
            await client.GetStream().WriteAsync(messageBytes, 0, messageBytes.Length);
        }

        private async Task BroadcastToRoom(GameRoom room, GameMessage message)
        {
            foreach (var player in room.Players)
            {
                await SendMessageToClient(player, message);
            }
        }

        private void RemoveClientFromRooms(TcpClient client)
        {
            foreach (var room in gameRooms.Values)
            {
                room.Players.Remove(client);
                if (room.Owner == client)
                {
                    // 방장이 나갔을 때의 처리 (예: 방 삭제 또는 새 방장 지정)
                }
            }
        }

        private async Task SendWelcomeMessage(NetworkStream stream )
        {
            GameMessage welcomeMessage = new GameMessage
            {
                Type = "RoomCreated",
                Data = stream.DataAvailable
            };

            string json = JsonConvert.SerializeObject(welcomeMessage);
            byte[] messageBytes = Encoding.UTF8.GetBytes(json);
            await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
        }
    }

    public class GameState
    {
        public List<PlayerState> Players { get; set; }
        public List<string> DrawPile { get; set; }
        public int CurrentPlayerIndex { get; set; }
        // 기타 필요한 게임 상태 정보...
    }

    public class PlayerState
    {
        public int Id { get; set; }
        public List<string> Hand { get; set; }
        public int Score { get; set; }
    }

    public class GameMessage
    {
        public string Type { get; set; }
        public object Data { get; set; }
    }
}