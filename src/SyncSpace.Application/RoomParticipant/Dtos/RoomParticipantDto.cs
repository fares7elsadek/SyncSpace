using SyncSpace.Domain.Entities;

namespace SyncSpace.Application.RoomParticipant.Dtos;

public class RoomParticipantDto
{
    public string ParticipantId { get; set; } = default!;
    public string RoomId { get; set; } = default!;
    public string UserId { get; set; } = default!;
    public DateTime JoinedAt { get; set; }
}
