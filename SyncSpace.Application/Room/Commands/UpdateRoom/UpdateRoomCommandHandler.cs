using AutoMapper;
using MediatR;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Application.Room.Dtos;
using SyncSpace.Domain.Constants;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;
using System.Reflection.Metadata;

namespace SyncSpace.Application.Room.Commands.UpdateRoom;

public class UpdateRoomCommandHandler(IUserContext userContext,
    IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<UpdateRoomCommand,RoomDto>
{
    public async Task<RoomDto> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        if (string.IsNullOrEmpty(user.userId))
            throw new CustomeException("User not authroized");

        var room = await unitOfWork.Room.GetOrDefalutAsync(r => r.RoomId == request.RoomId);
        if(room == null)
            throw new NotFoundException(nameof(room),request.RoomId);

        if (!user.IsInRole(UserRoles.Admin))
        {
            if (user.userId != room.HostUserId)
                throw new CustomeException("User not authroized");
        }
        if(!string.IsNullOrEmpty(request.RoomName))
            room.RoomName = request.RoomName;
        if(request.IsActive!=null)
            room.IsActive = request.IsActive.Value;
        await unitOfWork.SaveAsync();
        var roomDto = mapper.Map<RoomDto>(room);
        return roomDto;
    }
}
