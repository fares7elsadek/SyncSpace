using MediatR;
using System.Text.Json.Serialization;

namespace SyncSpace.Application.Stream.Commands.SyncStream;

public class SyncStreamCommand:IRequest
{
    [JsonIgnore]
    public string? RoomId { get; set; } 
    public double Time { get; set; } 
}
