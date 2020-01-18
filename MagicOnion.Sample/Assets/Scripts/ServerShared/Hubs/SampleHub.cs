using MagicOnion;
using MagicOnionSample.Shared.MessagePackObjects;
using System.Threading.Tasks;
using UnityEngine;

namespace MagicOnionSample.Shared.Hubs
{
    /// <summary>
    /// Client -> ServerのAPI
    /// </summary>
    public interface ISampleHub :IStreamingHub<ISampleHub, ISampleHubReceiver>
    {   
        /// <summary>
        /// ゲームに参加することをサーバーに伝える
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <returns></returns>
        Task JoinAsync(Player player);
        /// <summary>
        /// ゲームから切断することをサーバーに伝える
        /// </summary>
        /// <returns>鯖切断</returns>
        Task LeaveAsync();
        /// <summary>
        /// メッセージをサーバーに伝える
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <returns></returns>
        Task SendMessageAsync(string message);
        /// <summary>
        /// 移動したことをサーバーに伝える
        /// </summary>
        /// <param name="position">移動</param>
        /// <returns></returns>
        Task MovePositionAsnyc(Vector3 position);

    }

    public interface ISampleHubReceiver
    {
        /// <summary>
        /// 誰かがゲームに接続したことをクライアントに伝える
        /// </summary>
        /// <param name="name">joinしたプレイヤーの名前</param>
        void OnJoin(string name);
        /// <summary>
        /// 誰かがゲームから切断したことをクライアントに伝える
        /// </summary>
        /// <param name="name">leaveしたプレイヤーの名前</param>
        void OnLeave(string name);
        /// <summary>
        /// 誰かが発言したこををクライアントに伝える
        /// </summary>
        /// <param name="name">発言したプレイヤーの名前</param>
        /// <param name="message">メッセージの内容</param>
        void OnSendMessage(string name, string message);
        /// <summary>
        /// 誰かが移動したことをクライアントに伝える
        /// </summary>
        /// <param name="player">移動</param>
        void OnMovePosition(Player player);
    }
}
