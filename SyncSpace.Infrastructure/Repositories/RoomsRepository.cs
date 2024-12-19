using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Repositories;
using SyncSpace.Infrastructure.Data;

namespace SyncSpace.Infrastructure.Repositories;

public class RoomsRepository(AppDbContext db) : Repository<Rooms>(db), IRoomsRepository
{
    public void Update(Rooms rooms)
    {
        db.Update(rooms);
    }
}
