using AutoMapper;
using MediatR;
using SyncSpace.Application.Room.Dtos;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.Room.Queries.GetRoomById;

public class GetRoomByIdQueryHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetRoomByIdQuery, RoomDto>
{
    public async Task<RoomDto> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
    {
        var room = await unitOfWork.Room.GetOrDefalutAsync(p => p.RoomId == request.RoomId,
            IncludeProperties: "Participants,HostUser");
        if(room == null)
            throw new NotFoundException(nameof(room),request.RoomId);
        var roomDto = mapper.Map<RoomDto>(room);
        return roomDto;
    }
}
