using SyncSpace.Domain.Entities;

namespace SyncSpace.Domain.Repositories;

public interface IMessageRepository:IRepository<Messages>
{
    void Update(Messages message);
}
