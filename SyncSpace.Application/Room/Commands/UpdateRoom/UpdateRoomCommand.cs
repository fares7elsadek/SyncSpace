using MediatR;
using SyncSpace.Application.Room.Dtos;

namespace SyncSpace.Application.Room.Commands.UpdateRoom;

public class UpdateRoomCommand:IRequest<RoomDto>
{
    public string RoomId { get; set; } = default!;
    public string? RoomName { get; set; }
    public bool? IsActive { get; set; }
}
