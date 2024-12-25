using SyncSpace.Domain.Entities;

namespace SyncSpace.Domain.Repositories;

public interface IRoomsRepository:IRepository<Rooms>
{
    void Update(Rooms rooms);
}
