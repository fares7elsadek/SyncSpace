using Microsoft.AspNetCore.Identity;

namespace SyncSpace.Domain.Entities;

public class User:IdentityUser
{
    public User()
    {
        HostingRooms = new HashSet<Rooms>();
        ParticipatedRooms= new HashSet<RoomParticipants>();
        RoomMessages = new HashSet<Messages>();
        VideoSyncEvents = new HashSet<VideoSyncEvents>();
    }
    public string? Avatar { get; set; }
    public ICollection<Rooms> HostingRooms { get; set; }
    public ICollection<RoomParticipants> ParticipatedRooms { get; set; }
    public ICollection<Messages> RoomMessages { get; set; }
    public ICollection<VideoSyncEvents> VideoSyncEvents { get; set; }
    public List<RefreshToken>? RefreshTokens { get; set; }
}
