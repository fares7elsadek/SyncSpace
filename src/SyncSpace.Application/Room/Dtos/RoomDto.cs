using SyncSpace.Application.ApplicationUser.Dtos;
using SyncSpace.Application.ChatRoom.Dtos;
using SyncSpace.Application.RoomParticipant.Dtos;

namespace SyncSpace.Application.Room.Dtos;

public class RoomDto
{
    public string RoomId { get; set; } = default!;
    public string RoomName { get; set; } = default!;
    public string HostUserId { get; set; } = default!;
    public string? VideoUrl { get; set; }
    public double? CurrentTime { get; set; }
    public List<RoomParticipantDto> RoomParticipants { get; set; } = default!;
    public List<MessageDto> Messages { get; set; } = default!;
    public UserDto HostUser { get; set; } = default!;
    public bool IsPlaying { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; } = default!;
}
