using Microsoft.VisualBasic;

namespace SyncSpace.Domain.Entities;

public class Messages
{
    public string MessageId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public string RoomId { get; set; } = default!;
    public User User { get; set; } = default!;
    public Rooms Room { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime SentAt { get; set; } 
}
