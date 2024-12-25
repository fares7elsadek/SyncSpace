using MediatR;
using Microsoft.AspNetCore.Identity;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.Room.Commands.JoinRoom;

public class JoinRoomCommandHandler(IUserContext userContext,
    IUnitOfWork unitOfWork) : IRequestHandler<JoinRoomCommand>
{
    public async Task Handle(JoinRoomCommand request, CancellationToken cancellationToken)
    {
        var roomId = request.RoomId;
        var currUser = userContext.GetCurrentUser();
        if (currUser == null)
            throw new CustomeException("User not authorized");
        var room = await unitOfWork.Room.GetOrDefalutAsync(r => r.RoomId == roomId
        ,IncludeProperties: "Participants");
        if(room == null)
            throw new NotFoundException(nameof(room),roomId);
        if (room.Participants.Any(p => p.UserId == currUser.userId))
            throw new CustomeException("User already joined");

        var newParticipant = new RoomParticipants
        {
            RoomId = roomId,
            UserId = currUser.userId,
        };
        room.Participants.Add(newParticipant);
        await unitOfWork.SaveAsync();

    }
}
