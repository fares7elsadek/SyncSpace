namespace SyncSpace.Domain.Entities;

public class RoomParticipants
{
    public string ParticipantId { get; set; } = default!;
    public Rooms Room { get; set; } = default!;
    public User User { get; set; } = default!;
    public string RoomId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public DateTime JoinedAt { get; set; } 
}
