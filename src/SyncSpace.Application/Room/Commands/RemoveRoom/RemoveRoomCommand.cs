using MediatR;

namespace SyncSpace.Application.Room.Commands.RemoveRoom;

public class RemoveRoomCommand(string roomId) : IRequest
{
    public string RoomId { get; set; } = roomId;
}
