using MediatR;
using SyncSpace.Application.Room.Dtos;

namespace SyncSpace.Application.Room.Queries.GetAllRooms;

public class GetAllRoomsQuery:IRequest<IEnumerable<RoomDto>>
{
}
