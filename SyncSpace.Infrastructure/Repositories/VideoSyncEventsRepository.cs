using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Repositories;
using SyncSpace.Infrastructure.Data;

namespace SyncSpace.Infrastructure.Repositories;

public class VideoSyncEventsRepository(AppDbContext db) : Repository<VideoSyncEvents>(db), IVideoSyncEventsRepository
{
    public void Update(VideoSyncEvents videoSyncEvents)
    {
        db.Update(videoSyncEvents);
    }
}
