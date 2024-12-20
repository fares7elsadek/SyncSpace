using MediatR;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.Stream.Commands.PauseStream;

public class PauseStreamCommandHandler(IUserContext userContext,
    IUnitOfWork unitOfWork) : IRequestHandler<PauseStreamCommand>
{
    public async Task Handle(PauseStreamCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        if (user == null)
            throw new CustomeException("User is not authorized");
        var room = await unitOfWork.Room.GetOrDefalutAsync(x => x.RoomId == request.RoomId,
            IncludeProperties: "Participants");
        if (room == null)
            throw new NotFoundException(nameof(room), request.RoomId);
        if (!room.Participants.Any(u => u.UserId == user.userId) && user.userId!=room.HostUserId)
            throw new NotFoundException(nameof(room), user.userId);
        room.IsPlaying = false;
        unitOfWork.Room.Update(room);
        await unitOfWork.SaveAsync();
    }
}
