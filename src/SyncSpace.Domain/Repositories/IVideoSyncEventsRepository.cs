using SyncSpace.Domain.Entities;

namespace SyncSpace.Domain.Repositories;

public interface IVideoSyncEventsRepository:IRepository<VideoSyncEvents>
{
    void Update(VideoSyncEvents videoSyncEvents);
}
