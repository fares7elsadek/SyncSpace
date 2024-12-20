using AutoMapper;
using MediatR;
using SyncSpace.Application.Room.Dtos;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.Room.Queries.GetAllRooms;

public class GetAllRoomsQueryHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetAllRoomsQuery, IEnumerable<RoomDto>>
{
    public async Task<IEnumerable<RoomDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    {
        var rooms =  await unitOfWork.Room.GetAllAsync(IncludeProperties: "Participants,HostUser");
        var roomsDto = mapper.Map<IEnumerable<RoomDto>>(rooms);
        return roomsDto;
    }
}
