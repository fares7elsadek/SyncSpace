using AutoMapper;
using MediatR;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Application.Stream.Dtos;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.Stream.Queries.GetStreamDetails;

public class GetStreamDetailsQueryHandler(IUserContext userContext,
    IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<GetStreamDetailsQuery, StreamDetails>
{
    public async Task<StreamDetails> Handle(GetStreamDetailsQuery request, CancellationToken cancellationToken)
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
        var StreamDetails = mapper.Map<StreamDetails>(room);
        return StreamDetails;
    }
}
