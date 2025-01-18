using Microsoft.AspNetCore.SignalR;
using Serilog;
using SyncSpace.Domain.Helpers;
using SyncSpace.Domain.Repositories;
using System.Collections.Concurrent;

namespace SyncSpace.API.SignalR.Hubs
{
    public class StreamingHub : Hub
    {
        private static Dictionary<string, RoomState> _roomStates = new();
        public override async Task OnConnectedAsync()
        {
            Log.Warning($"Connected with connectionId {Context.ConnectionId}");
            await Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

            if (!_roomStates.ContainsKey(roomId))
            {
                _roomStates[roomId] = new RoomState
                {
                    CurrentTime = 0,
                    IsPaused = true,
                    LastUpdated = DateTime.UtcNow
                };
            }

            var roomState = _roomStates[roomId];

            double currentPlaybackTime = GetCalculatedPlaybackTime(roomState);

            await Clients.Caller.SendAsync("SyncVideo", currentPlaybackTime, roomState.IsPaused, DateTime.UtcNow);
        }

        public async Task UpdateVideoState(string roomId, double currentTime, bool isPaused)
        {
            if (_roomStates.ContainsKey(roomId))
            {
                var roomState = _roomStates[roomId];

                roomState.CurrentTime = currentTime;
                roomState.IsPaused = isPaused;
                roomState.LastUpdated = DateTime.UtcNow;
                await Clients.Group(roomId).SendAsync("UpdateVideo", currentTime, isPaused, DateTime.UtcNow);
            }
        }

        public async Task ResyncRoom(string roomId)
        {
            if (_roomStates.ContainsKey(roomId))
            {
                var roomState = _roomStates[roomId];
                double currentPlaybackTime = GetCalculatedPlaybackTime(roomState);
                await Clients.Group(roomId).SendAsync("SyncVideo", currentPlaybackTime, roomState.IsPaused, DateTime.UtcNow);
            }
        }

        private double GetCalculatedPlaybackTime(RoomState roomState)
        {
            if (roomState.IsPaused)
            {
                return roomState.CurrentTime;
            }
            else
            {
                return roomState.CurrentTime + (DateTime.UtcNow - roomState.LastUpdated).TotalSeconds;
            }
        }

        public async Task LeaveRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).SendAsync("UserLeft", Context.ConnectionId);
        }

        public async Task StartStream(string roomId, string streamUrl)
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

        public async Task SyncStream(string roomId, double currentTime)
        {
            await Clients.Group(roomId).SendAsync("StreamSynced", currentTime);
        }
    }
}