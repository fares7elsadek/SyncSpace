using SyncSpace.Domain.Constants;

namespace SyncSpace.Domain.Entities;

public class VideoSyncEvents
{
    public string EventId { get; set; } = default!;
    public string RoomId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public Rooms Room { get; set; } = default!;
    public User User { get; set; } = default!;
    public EventType EventType { get; set; }
    public double Time { get; set; }
    public DateTime TriggeredAt { get; set; }
}
