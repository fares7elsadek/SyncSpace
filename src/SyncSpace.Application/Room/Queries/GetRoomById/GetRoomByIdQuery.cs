using MediatR;
using SyncSpace.Application.Room.Dtos;

namespace SyncSpace.Application.Room.Queries.GetRoomById;

public class GetRoomByIdQuery(string roomId):IRequest<RoomDto>
{
    public string RoomId { get; set; } = roomId;
}
