using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Repositories;
using SyncSpace.Infrastructure.Data;

namespace SyncSpace.Infrastructure.Repositories;

public class RoomParticipantsRepository(AppDbContext db) : Repository<RoomParticipants>(db), IRoomParticipantsRepository
{
    public void Update(RoomParticipants roomParticipants)
    {
        db.Update(roomParticipants);
    }
}
