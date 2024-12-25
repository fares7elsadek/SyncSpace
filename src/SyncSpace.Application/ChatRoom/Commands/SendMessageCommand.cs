using MediatR;
using SyncSpace.Application.ChatRoom.Dtos;
using SyncSpace.Domain.Entities;

namespace SyncSpace.Application.ChatRoom.Commands;

public class SendMessageCommand:IRequest<MessageDto>
{
    public string RoomId { get; set; } = default!;
    public string Content { get; set; } = default!;
}
