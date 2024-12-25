using MediatR;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.Stream.Commands.StartStream;

public class StartStreamCommandHandler(IUserContext userContext,
    IUnitOfWork unitOfWork) : IRequestHandler<StartStreamCommand>
{
    public async Task Handle(StartStreamCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        if (user == null)
            throw new CustomeException("User is not authorized");
        var room = await unitOfWork.Room.GetOrDefalutAsync(x => x.RoomId == request.RoomId,
            IncludeProperties: "Participants");
        if(room == null)
            throw new NotFoundException(nameof(room),request.RoomId);
        if(!room.Participants.Any(u => u.UserId == user.userId) && user.userId != room.HostUserId)
            throw new NotFoundException(nameof(user),user.userId);
        room.VideoUrl = request.VideoUrl;
        room.IsPlaying = true;
        room.CurrentVideoTime = TimeSpan.Zero;
        room.IsActive = true;
        unitOfWork.Room.Update(room);
        await unitOfWork.SaveAsync();
    }
}
