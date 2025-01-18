using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Application.Room.Dtos;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.Room.Commands.CreateRoom;

public class CreateRoomCommandHandler(IUserContext userContext
    , UserManager<User> userManager, IMapper mapper,IUnitOfWork unitOfWork) : IRequestHandler<CreateRoomCommand, RoomDto>
{
    public async Task<RoomDto> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var userId = userContext.GetCurrentUser().userId;
        if (string.IsNullOrEmpty(userId))
            throw new CustomeException("User not authorized");

        var user = await userManager.FindByIdAsync(userId);
        if(user == null)
            throw new CustomeException("User not authorized");
        var roomId = Guid.NewGuid().ToString();
        await unitOfWork.Room.AddAsync(new Rooms
        {
            RoomId = roomId,
            HostUserId = userId,
            RoomName = request.RoomName
        });
        await unitOfWork.SaveAsync();
        var room = await unitOfWork.Room.GetOrDefalutAsync(r => r.RoomId == roomId,IncludeProperties: "Participants");
        var newParticipant = new RoomParticipants
        {
            RoomId = roomId,
            UserId = userId,
        };
        room.Participants.Add(newParticipant);
        await unitOfWork.SaveAsync();
        var roomDto = mapper.Map<RoomDto>(room);
        return roomDto;
    }
}
