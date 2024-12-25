using MediatR;
using System.Text.Json.Serialization;

namespace SyncSpace.Application.Stream.Commands.StartStream;

public class StartStreamCommand:IRequest
{
    [JsonIgnore]
    public string? RoomId { get; set; } = default!;
    public string VideoUrl { get; set; } = default!;
}
