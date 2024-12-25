using MediatR;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.Stream.Commands.SyncStream;

public class SyncStreamCommandHandler(IUserContext userContext,
    IUnitOfWork unitOfWork) : IRequestHandler<SyncStreamCommand>
{
    public async Task Handle(SyncStreamCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        if (user == null)
            throw new CustomeException("User is not authorized");
        var room = await unitOfWork.Room.GetOrDefalutAsync(x => x.RoomId == request.RoomId,
            IncludeProperties: "Participants");
        if (room == null)
            throw new NotFoundException(nameof(room), request.RoomId);
        if (!room.Participants.Any(u => u.UserId == user.userId) && user.userId != room.HostUserId)
            throw new NotFoundException(nameof(room), user.userId);

        room.CurrentVideoTime = request.Time;
        unitOfWork.Room.Update(room);
        await unitOfWork.SaveAsync();
    }
}
