using MediatR;
using System.Text.Json.Serialization;

namespace SyncSpace.Application.Room.Commands.JoinRoom;

public class JoinRoomCommand:IRequest
{
    [JsonIgnore]
    public string? RoomId { get; set; }
    public string ConnectionId { get; set; } = default!;
}
