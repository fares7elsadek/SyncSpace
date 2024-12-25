using MediatR;
using System.Text.Json.Serialization;

namespace SyncSpace.Application.Room.Commands.LeaveRoom;

public class LeaveRoomCommand:IRequest
{
    [JsonIgnore]
    public string? RoomId { get; set; }
    public string ConnectionId { get; set; } = default!;
}
