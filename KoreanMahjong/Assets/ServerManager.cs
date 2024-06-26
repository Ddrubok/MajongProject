using UnityEngine;
using System.Threading.Tasks;

public class ServerManager : MonoBehaviour
{
    public GameClient gameClient;
    public string serverIP = "localhost"; // 서버 IP 주소
    public int serverPort = 8888; // 서버 포트 번호

    async void Start()
    {
        await ConnectToServer();
    }

    async Task ConnectToServer()
    {
        try
        {
            await gameClient.ConnectToServer(serverIP, serverPort);
            Debug.Log("서버에 연결되었습니다.");
            // 연결 성공 후 추가 로직
        }
        catch (System.Exception e)
        {
            Debug.LogError($"서버 연결 실패: {e.Message}");
            // 연결 실패 시 처리 (예: 재시도 또는 에러 메시지 표시)
        }
    }

    // 게임 플레이 관련 메서드들...
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