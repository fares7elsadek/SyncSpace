using AutoMapper;
using MediatR;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Application.Room.Dtos;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.Room.Queries.GetUserRooms;

public class GetUserRoomsQueryHandler(IUserContext userContext,
    IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<GetUserRoomsQuery, IEnumerable<RoomDto>>
{
    public async Task<IEnumerable<RoomDto>> Handle(GetUserRoomsQuery request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        var UserParticipantRoom = unitOfWork.RoomParticipants.GetAllWithConditionAsync(u => u.UserId == user.userId).Result.Select(x => x.RoomId);
        var UserRooms = await unitOfWork.Room.GetAllWithConditionAsync(x => UserParticipantRoom.Contains(x.RoomId)); 
        var roomsDto = mapper.Map<IEnumerable<RoomDto>>(UserRooms);
        return roomsDto;
    }
}
