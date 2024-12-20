using MediatR;
using SyncSpace.Application.ChatRoom.Dtos;

namespace SyncSpace.Application.ChatRoom.Queries.GetRoomChat;

public class GetRoomChatQuery(string roomId):IRequest<IEnumerable<MessageDto>>
{
    public string RoomId { get; set; } = roomId;
}
