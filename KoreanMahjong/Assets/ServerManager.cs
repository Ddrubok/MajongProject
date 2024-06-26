using UnityEngine;
using System.Threading.Tasks;

public class ServerManager : MonoBehaviour
{
    public GameClient gameClient;
    public string serverIP = "localhost"; // ���� IP �ּ�
    public int serverPort = 8888; // ���� ��Ʈ ��ȣ

    async void Start()
    {
        await ConnectToServer();
    }

    async Task ConnectToServer()
    {
        try
        {
            await gameClient.ConnectToServer(serverIP, serverPort);
            Debug.Log("������ ����Ǿ����ϴ�.");
            // ���� ���� �� �߰� ����
        }
        catch (System.Exception e)
        {
            Debug.LogError($"���� ���� ����: {e.Message}");
            // ���� ���� �� ó�� (��: ��õ� �Ǵ� ���� �޽��� ǥ��)
        }
    }

    // ���� �÷��� ���� �޼����...
    public async Task DrawTile()
    {
        await gameClient.DrawTile();
    }

    public async Task DiscardTile(string tile)
    {
        await gameClient.DiscardTile(tile);
    }

    public async Task CompleteWord(string word)
    {
        await gameClient.CompleteWord(word);
    }
}