namespace SyncSpace.Domain.Entities;

public class Rooms
{
    public Rooms()
    {
        Participants = new HashSet<RoomParticipants>();
        Messages = new HashSet<Messages>();
        VideoSyncEvents = new HashSet<VideoSyncEvents>();
    }
    public string RoomId { get; set; } = default!;
    public string RoomName { get; set; } = default!;
    public User HostUser { get; set; } = default!;
    public string HostUserId { get; set; } = default!;
    public string? VideoUrl { get; set; } 
    public decimal? CurrentTime { get; set; }
    public bool IsPlaying { get; set; } 
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = default!;
    public ICollection<RoomParticipants> Participants { get; set; }
    public ICollection<Messages> Messages { get; set; }
    public ICollection<VideoSyncEvents> VideoSyncEvents { get; set; }
}
