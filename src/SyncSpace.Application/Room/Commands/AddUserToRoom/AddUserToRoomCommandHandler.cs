using MediatR;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Domain.Constants;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.Room.Commands.AddUserToRoom;

public class AddUserToRoomCommandHandler(IUserContext userContext,
    IUnitOfWork unitOfWork) : IRequestHandler<AddUserToRoomCommand>
{
    public async Task Handle(AddUserToRoomCommand request, CancellationToken cancellationToken)
    {
        var roomId = request.RoomId;
        var userId = request.UserId;
        var currUser = userContext.GetCurrentUser();
        if (currUser == null)
            throw new CustomeException("User not authorized");
        var room = await unitOfWork.Room.GetOrDefalutAsync(r => r.RoomId == roomId,
            IncludeProperties: "Participants");
        if(room == null)
            throw new NotFoundException(nameof(room),roomId);
        if (!currUser.IsInRole(UserRoles.Admin))
        {
            if(currUser.userId != room.HostUserId)
                throw new CustomeException("User not authorized");
        }

        if(room.Participants.Any(p => p.UserId == userId))
            throw new CustomeException("User already joined");

        var newId = Guid.NewGuid().ToString();
        var roomParticipant = new RoomParticipants
        {
            ParticipantId = newId,
            RoomId = roomId,
            UserId = userId,
        };
        await unitOfWork.RoomParticipants.AddAsync(roomParticipant);
        await unitOfWork.SaveAsync();
    }
}
