using Grpc.Core;
using MagicOnion.Client;
using MagicOnionSample.Shared.Hubs;
using MagicOnionSample.Shared.MessagePackObjects;
using MagicOnionSample.Shared.Services;
using UnityEngine;

public class SampleController : MonoBehaviour
{
    private Channel channel;
    private ISampleService sampleService;
    private ISampleHub sampleHub;
    //private ISampleHubReceiver sampleHubReceiver;

    void Start()
    {
        this.channel = new Channel("localhost:12345", ChannelCredentials.Insecure);
        this.sampleService = MagicOnionClient.Create<ISampleService>(channel);
        this.sampleHub = StreamingHubClient.Connect<ISampleHub, ISampleHubReceiver>(this.channel, this);

        //普通のAPI呼び出し
        //this.SampleServiceTest(1,2);

        //リアルタイム通信のテスト用
        SampleHubTest();
    }

    async void OnDestroy()
    {
        await this.channel.ShutdownAsync();
    }

    /// <summary>
    /// 普通のAPI通信用テストmethod
    /// </summary>
    /// <param name="x">テスト用数値X</param>
    /// <param name="y">テスト用数値Y</param>
    async void SampleServiceTest(int x,int y)
    {
        var sumResult = await this.sampleService.SumAsync(x, y);
        Debug.Log($"{nameof(sumResult)}: {sumResult}");

        var productResult = await this.sampleService.ProductAsync(2, 3);
        Debug.Log($"{nameof(productResult)}: {productResult}");
    }

    /// <summary>
    /// リアルタイム通信のテスト用メソッド
    /// </summary>
    async void SampleHubTest()
    {
        // 自分のプレイヤー情報
        var player = new Player
        {
            Name = "lyla",
            Position = new Vector3(0, 0, 0),
            Rotation = new Quaternion(0, 0, 0, 0)
        };

        //ゲームに接続する
        await this.sampleHub.JoinAsync(player);

        //チャットで発言する
        await this.sampleHub.SendMessageAsync("hello world");

        //位置情報を更新する
        player.Position = new Vector3(1, 0, 0);
        await this.sampleHub.MovePositionAsnyc(player.Position);

        //ゲームから切断する
        await this.sampleHub.LeaveAsync();
    }

    #region リアルタイム通信でサーバーから呼ばれるmethod群

    public void OnJoin(string name)
    {
        Debug.Log($"{name}さんが入室しました");
    }

    public void OnLeave(string name)
    {
        Debug.Log($"{name}さんが退室しました");
    }

    public void OnMessage(string name, string message)
    {
        Debug.Log($"{name}: {message}");
    }

    public void OnMovePosition(Player player)
    {
        Debug.Log($"{player.Name}さんが移動しました: {{x: {player.Position.x}, y: {player.Position.y}, z: {player.Position.z}");
    }
    #endregion
}
