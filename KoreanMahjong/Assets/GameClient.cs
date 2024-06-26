using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
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
    // ��Ÿ �ʿ��� ���� ���� ����...
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
    private GameState gameState;

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

    //private void ProcessServerMessage(string message)
    //{
    //    gameState = JsonConvert.DeserializeObject<GameState>(message);
    //    UpdateGameUI();
    //}
    private void ProcessServerMessage(string message)
    {
        GameMessage gameMessage = JsonConvert.DeserializeObject<GameMessage>(message);

        switch (gameMessage.Type)
        {
            case "Welcome":
                Debug.Log(gameMessage.Data as string);
                // �ʿ��ϴٸ� ���⿡ �߰� ���� ���� (��: UI�� ȯ�� �޽��� ǥ��)
                break;
            case "GameState":
                gameState = JsonConvert.DeserializeObject<GameState>(gameMessage.Data.ToString());
                UpdateGameUI();
                break;
                // �ٸ� �޽��� Ÿ�Ե� ó��...
        }
    }
    private void UpdateGameUI()
    {
        // ���� UI ������Ʈ ����
    }

    public async Task SendMessageToServer(GameMessage message)
    {
        string json = JsonConvert.SerializeObject(message);
        byte[] buffer = Encoding.UTF8.GetBytes(json);
        await stream.WriteAsync(buffer, 0, buffer.Length);
    }

    // ���� �׼� �޼����
    public async Task DrawTile()
    {
        await SendMessageToServer(new GameMessage { Type = "DrawTile" });
    }

    public async Task DiscardTile(string tile)
    {
        await SendMessageToServer(new GameMessage { Type = "DiscardTile", Data = tile });
    }

    public async Task CompleteWord(string word)
    {
        await SendMessageToServer(new GameMessage { Type = "CompleteWord", Data = word });
    }

    // ��Ÿ �ʿ��� ���� �׼� �޼����...
}