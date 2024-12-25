using AutoMapper;
using MediatR;
using SyncSpace.Application.ChatRoom.Dtos;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.ChatRoom.Queries.GetRoomChat;

public class GetRoomChatQueryHandler(IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<GetRoomChatQuery, IEnumerable<MessageDto>>
{
    public async Task<IEnumerable<MessageDto>> Handle(GetRoomChatQuery request, CancellationToken cancellationToken)
    {
       var room = await unitOfWork.Room.GetOrDefalutAsync(r => r.RoomId== request.RoomId,
           IncludeProperties:"Messages.User");
       
       if(room==null)
            throw new NotFoundException(nameof(room),request.RoomId);

       var messages = mapper.Map<IEnumerable<MessageDto>>(room.Messages);
       return messages;
    }
}
