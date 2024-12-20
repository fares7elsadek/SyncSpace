using MediatR;

namespace SyncSpace.Application.Room.Commands.LeaveRoom;

public class LeaveRoomCommand(string roomId):IRequest
{
    public string RoomId { get; set; } = roomId;
}
