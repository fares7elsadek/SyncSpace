using SyncSpace.Domain.Entities;

namespace SyncSpace.Domain.Repositories;

public interface IRoomParticipantsRepository:IRepository<RoomParticipants>
{
    void Update(RoomParticipants roomParticipants);
}
