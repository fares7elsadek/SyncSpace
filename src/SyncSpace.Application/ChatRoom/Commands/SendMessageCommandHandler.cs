using AutoMapper;
using MediatR;
using SyncSpace.Application.ApplicationUser;
using SyncSpace.Application.ChatRoom.Dtos;
using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Exceptions;
using SyncSpace.Domain.Repositories;

namespace SyncSpace.Application.ChatRoom.Commands;

public class SendMessageCommandHandler(IUserContext userContext,
    IUnitOfWork unitOfWork,IMapper mapper) : IRequestHandler<SendMessageCommand,MessageDto>
{
    public async Task<MessageDto> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();
        var room = await unitOfWork.Room.GetOrDefalutAsync(p => p.RoomId == request.RoomId,
            IncludeProperties: "Participants,Messages");
        if (room == null)
            throw new NotFoundException(nameof(room),request.RoomId);
        if (!room.Participants.Any(p => p.UserId == user.userId))
            throw new CustomeException("User not exist in this room");

        var newguid = Guid.NewGuid().ToString();
        var newMessage = new Messages
        {
            MessageId = newguid,
            UserId = user.userId,
            RoomId = request.RoomId,
            Content = request.Content,
            SentAt = DateTime.UtcNow,
        };
        await unitOfWork.Messages.AddAsync(newMessage);
        await unitOfWork.SaveAsync();
        var message = await unitOfWork.Messages.GetOrDefalutAsync(m => m.MessageId == newguid,
            IncludeProperties:"User");
        var messageDto = mapper.Map<MessageDto>(message);
        return messageDto;
    }
}
