namespace SyncSpace.Application.Stream.Dtos;

public class StreamDetails
{
    public string VideoUrl { get; set; } = default!;
    public TimeSpan CurrentVideoTime { get; set; } = default!;
    public bool IsPlaying { get; set; }
}
