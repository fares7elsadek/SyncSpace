using Microsoft.AspNetCore.SignalR;

namespace SyncSpace.API.SignalR.Hubs;

public class StreamingHub:Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("hey", "hello");
    }
    public async Task JoinRoom(string roomId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        await Clients.Group(roomId).SendAsync("UserJoined", Context.ConnectionId);
    }

    public async Task LeaveRoom(string roomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        await Clients.Group(roomId).SendAsync("UserLeft", Context.ConnectionId);
    }

    public async Task StartStream(string roomId,string streamUrl)
    {
        await Clients.Group(roomId).SendAsync("StreamStarted", streamUrl);
    }

    public async Task ChangeStream(string roomId, string newStreamUrl)
    {
        await Clients.Group(roomId).SendAsync("StreamChanged", newStreamUrl);
    }

    public async Task PauseStream(string roomId)
    {
        await Clients.Group(roomId).SendAsync("StreamPaused");
    }

    public async Task PlayStream(string roomId)
    {
        await Clients.Group(roomId).SendAsync("StreamResumed");
    }

    public async Task SyncStream(string roomId, TimeSpan currentTime)
    {
        await Clients.Group(roomId).SendAsync("StreamSynced", currentTime);
    }
}
