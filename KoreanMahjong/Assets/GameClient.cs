using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

public class GameMessage
{
    public string Type { get; set; }
    public object Data { get; set; }
}

public class GameState
{
    public List<PlayerState> Players { get; set; }
    public List<string> DrawPile { get; set; }
    public int CurrentPlayerIndex { get; set; }
}

public class PlayerState
{
    public int Id { get; set; }
    public List<string> Hand { get; set; }
    public int Score { get; set; }
}
public class GameClient : MonoBehaviour
{
    private TcpClient tcpClient;
    private NetworkStream stream;
    private string currentRoomId;

    public async Task ConnectToServer(string ip, int port)
    {
        tcpClient = new TcpClient();
        await tcpClient.ConnectAsync(ip, port);
        stream = tcpClient.GetStream();
        Debug.Log("Connected to server");
        _ = ReceiveMessagesAsync();
    }

    private async Task ReceiveMessagesAsync()
    {
        byte[] buffer = new byte[1024];
        while (true)
        {
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead == 0) break;
            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            ProcessServerMessage(message);
        }
    }

    private void ProcessServerMessage(string message)
    {
        GameMessage gameMessage = JsonConvert.DeserializeObject<GameMessage>(message);

        switch (gameMessage.Type)
        {
            case "RoomCreated":
                currentRoomId = gameMessage.Data.ToString();
                Debug.Log($"Room created with ID: {currentRoomId}");
                break;
            case "JoinedRoom":
                currentRoomId = gameMessage.Data.ToString();
                Debug.Log($"Joined room with ID: {currentRoomId}");
                break;
            case "PlayerJoined":
                int playerCount = (int)gameMessage.Data;
                Debug.Log($"Player joined. Total players: {playerCount}");
                break;
            case "GameStarted":
                Debug.Log("Game started!");
                break;
            case "RoomFull":
                Debug.Log("Room is full");
                break;
            case "RoomNotFound":
                Debug.Log("Room not found");
                break;
        }
    }

    public async Task CreateRoom()
    {
        await SendMessageToServer(new GameMessage { Type = "CreateRoom" });
    }

    public async Task JoinRoom(string roomId)
    {
        await SendMessageToServer(new GameMessage { Type = "JoinRoom", Data = roomId });
    }

    private async Task SendMessageToServer(GameMessage message)
    {
        string json = JsonConvert.SerializeObject(message);
        byte[] buffer = Encoding.UTF8.GetBytes(json);
        await stream.WriteAsync(buffer, 0, buffer.Length);
    }
}
