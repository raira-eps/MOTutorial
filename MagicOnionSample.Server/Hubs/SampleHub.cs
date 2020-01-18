using MagicOnion.Server.Hubs;
using MagicOnionSample.Shared.Hubs;
using MagicOnionSample.Shared.MessagePackObjects;
using UnityEngine;
using System.Threading.Tasks;

public class SampleHub : StreamingHubBase<ISampleHub, ISampleHubReceiver>, ISampleHub
{
    IGroup room;
    Player me;

    public async Task JoinAsync(Player player)
    {
        //roomは全員固定
        const string roomName = "SampleRoom";
        //roomに参加＆ルームを保持
        this.room = await this.Group.AddAsync(roomName);
        //自分の情報も保持
        me = player;
        //参加したことをroomに参加してる全メンバーに通知
        this.Broadcast(room).OnJoin(me.Name);
    }

    public async Task LeaveAsync()
    {
        //room内のメンバーから自分を削除
        await room.RemoveAsync(this.Context);
        //退室したことを全メンバーに通知
        this.Broadcast(room).OnLeave(me.Name);
    }

    public async Task SendMessageAsync(string message)
    {
        //発言した内容を全メンバーに通知
        this.Broadcast(room).OnSendMessage(me.Name, message);
    }

    public async Task MovePositionAsnyc(Vector3 position)
    {
        //server上の情報を更新
        me.Position = position;
        //更新したプレイヤーの情報を全メンバーに通知
        this.Broadcast(room).OnMovePosition(me);
    }

    protected override ValueTask OnDisconnected()
    {
        //nop
        return CompletedTask;
    }
}
