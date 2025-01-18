using MediatR;
using SyncSpace.Application.Room.Dtos;

namespace SyncSpace.Application.Room.Queries.GetUserRooms;

public class GetUserRoomsQuery:IRequest<IEnumerable<RoomDto>>
{
}
