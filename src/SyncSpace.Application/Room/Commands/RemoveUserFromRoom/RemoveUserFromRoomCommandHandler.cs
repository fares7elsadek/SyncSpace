using MediatR;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Domain.Constants;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.Room.Commands.RemoveUserFromRoom;

public class RemoveUserFromRoomCommandHandler(IUserContext userContext,
    IUnitOfWork unitOfWork) : IRequestHandler<RemoveUserFromRoomCommand>
{
    public async Task Handle(RemoveUserFromRoomCommand request, CancellationToken cancellationToken)
    {
        var roomId = request.RoomId;
        var userId = request.UserId;
        var currUser = userContext.GetCurrentUser();
        if (currUser == null)
            throw new CustomeException("User not authorized");
        var room = await unitOfWork.Room.GetOrDefalutAsync(r => r.RoomId == roomId,
            IncludeProperties: "Participants");
        if (room == null)
            throw new NotFoundException(nameof(room), roomId);
        if (!currUser.IsInRole(UserRoles.Admin))
        {
            if (currUser.userId != room.HostUserId)
                throw new CustomeException("User not authorized");
        }

        if (!room.Participants.Any(p => p.UserId == userId))
            throw new CustomeException("User does not exsit in this room");
        var userParticipant = room.Participants.SingleOrDefault(p  => p.UserId == userId);
        if (userParticipant == null)
            throw new NotFoundException(nameof(userParticipant),request.UserId);
        unitOfWork.RoomParticipants.Remove(userParticipant);
        await unitOfWork.SaveAsync();
    }
}
