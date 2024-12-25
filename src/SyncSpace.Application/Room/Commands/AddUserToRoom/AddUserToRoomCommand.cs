

using MediatR;

namespace SyncSpace.Application.Room.Commands.AddUserToRoom;

public class AddUserToRoomCommand(string roomId,string userId):IRequest
{
    public string RoomId { get; set; } = roomId;
    public string UserId { get; set; } = userId;
}
