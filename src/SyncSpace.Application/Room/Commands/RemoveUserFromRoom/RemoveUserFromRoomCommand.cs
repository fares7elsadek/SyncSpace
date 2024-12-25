using MediatR;

namespace SyncSpace.Application.Room.Commands.RemoveUserFromRoom;

public class RemoveUserFromRoomCommand(string roomId, string userId) :IRequest
{
    public string RoomId { get; set; } = roomId;
    public string UserId { get; set; } = userId;
}
