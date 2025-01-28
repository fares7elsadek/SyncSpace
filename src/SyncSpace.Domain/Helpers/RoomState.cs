namespace SyncSpace.Domain.Helpers;

public class RoomState
{
    public double CurrentTime { get; set; } 
    public bool IsPaused { get; set; }
    public string VideoUrl { get; set; } = default!;
    public DateTime LastUpdated { get; set; }
}
