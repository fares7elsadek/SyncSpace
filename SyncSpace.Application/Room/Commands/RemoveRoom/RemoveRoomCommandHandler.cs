using MediatR;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Domain.Constants;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.Room.Commands.RemoveRoom;

public class RemoveRoomCommandHandler(IUserContext userContext,
    IUnitOfWork unitOfWork) : IRequestHandler<RemoveRoomCommand>
{
    public async Task Handle(RemoveRoomCommand request, CancellationToken cancellationToken)
    {
        var roomId = request.RoomId;
        var room = await unitOfWork.Room.GetOrDefalutAsync(r => r.RoomId == roomId);
        if(room == null)
            throw new NotFoundException(nameof(room),request.RoomId);

        var user = userContext.GetCurrentUser();
        if (!user.IsInRole(UserRoles.Admin))
        {
            if (user.userId != room.HostUserId)
                throw new CustomeException("User is not authroized");
        }
        if (room.IsActive == true)
            throw new CustomeException("The room is active you must deactivated first");
        unitOfWork.Room.Remove(room);
        await unitOfWork.SaveAsync();
    }
}
