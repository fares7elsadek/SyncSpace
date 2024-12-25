using SyncSpace.Domain.Entities;

namespace SyncSpace.Domain.Repositories;

public interface IUserRepository:IRepository<User>
{
    void Update(User user);
}
