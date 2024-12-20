using SyncSpace.Application.ApplicationUser.Dtos;

namespace SyncSpace.Application.ChatRoom.Dtos;

public class MessageDto
{
    public string MessageId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string RoomId { get; set; } = default!;
    public UserDto User { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime SentAt { get; set; }
}
