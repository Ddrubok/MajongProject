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

public class GameServer
    {
        private TcpListener tcpListener;
        private List<TcpClient> clients = new List<TcpClient>();
        private GameState gameState;

        public async Task StartServer(int port)
        {
            tcpListener = new TcpListener(IPAddress.Any, port);
            tcpListener.Start();
            Console.WriteLine($"Server started on port {port}");

            while (true)
            {
                TcpClient client = await tcpListener.AcceptTcpClientAsync();
                clients.Add(client);
                _ = HandleClientAsync(client);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                using (NetworkStream stream = client.GetStream())
                {
                    // 환영 메시지 전송
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
                clients.Remove(client);
                client.Close();
            }
        }

        private async Task ProcessMessageAsync(string message, TcpClient sender)
        {
            GameMessage gameMessage = JsonConvert.DeserializeObject<GameMessage>(message);

            switch (gameMessage.Type)
            {
                case "DrawTile":
                    // 타일 드로우 로직
                    break;
                case "DiscardTile":
                    // 타일 버리기 로직
                    break;
                case "CompleteWord":
                    // 단어 완성 확인 및 점수 계산 로직
                    break;
                    // 기타 필요한 메시지 타입들...
            }

            // 게임 상태 업데이트 및 모든 클라이언트에게 전송
            await BroadcastGameStateAsync();
        }

        private async Task BroadcastGameStateAsync()
        {
            string stateJson = JsonConvert.SerializeObject(gameState);
            byte[] stateBuffer = Encoding.UTF8.GetBytes(stateJson);

            foreach (TcpClient client in clients)
            {
                NetworkStream stream = client.GetStream();
                await stream.WriteAsync(stateBuffer, 0, stateBuffer.Length);
            }
        }

        private async Task SendWelcomeMessage(NetworkStream stream)
        {
            GameMessage welcomeMessage = new GameMessage
            {
                Type = "Welcome",
                Data = "환영합니다! 한국어 알파벳 마작 게임 서버에 연결되었습니다."
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
