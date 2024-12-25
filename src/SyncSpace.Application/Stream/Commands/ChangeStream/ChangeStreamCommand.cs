using MediatR;
using System.Text.Json.Serialization;

namespace SyncSpace.Application.Stream.Commands.ChangeStream;

public class ChangeStreamCommand:IRequest
{
    [JsonIgnore]
    public string? RoomId { get; set; } = default!;
    public string VideoUrl { get; set; } = default!;
}
