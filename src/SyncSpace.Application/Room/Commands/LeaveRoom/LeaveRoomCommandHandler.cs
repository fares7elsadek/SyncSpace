using MediatR;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.Room.Commands.LeaveRoom;

public class LeaveRoomCommandHandler(IUserContext userContext,
    IUnitOfWork unitOfWork) : IRequestHandler<LeaveRoomCommand>
{
    public async Task Handle(LeaveRoomCommand request, CancellationToken cancellationToken)
    {
        var roomId = request.RoomId;
        var currUser = userContext.GetCurrentUser();
        if (currUser == null)
            throw new CustomeException("User not authorized");
        var room = await unitOfWork.Room.GetOrDefalutAsync(r => r.RoomId == roomId
        , IncludeProperties: "Participants");
        if (room == null)
            throw new NotFoundException(nameof(room), roomId);
        if (!room.Participants.Any(p => p.UserId == currUser.userId))
            throw new CustomeException("User is not exsit in this room");
        var participant = room.Participants.SingleOrDefault(p => p.UserId == currUser.userId);
        if (participant == null)
            throw new CustomeException("Something wrong has happened");
        unitOfWork.RoomParticipants.Remove(participant);
        await unitOfWork.SaveAsync();
    }
}
