using MediatR;

namespace SyncSpace.Application.Room.Commands.JoinRoom;

public class JoinRoomCommand(string roomId):IRequest
{
    public string RoomId { get; set; } = roomId;
}
