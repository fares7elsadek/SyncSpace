using MediatR;
using SyncSpace.Application.Room.Dtos;

namespace SyncSpace.Application.Room.Commands.CreateRoom;

public class CreateRoomCommand:IRequest<RoomDto>
{
    public string RoomName { get; set; } = default!;
}
