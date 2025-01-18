namespace SyncSpace.Domain.Helpers;

public class RoomState
{
    public double CurrentTime { get; set; } 
    public bool IsPaused { get; set; }

    public DateTime LastUpdated { get; set; }
}
