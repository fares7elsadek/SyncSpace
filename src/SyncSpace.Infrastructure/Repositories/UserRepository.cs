using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Repositories;
using SyncSpace.Infrastructure.Data;

namespace SyncSpace.Infrastructure.Repositories;

public class UserRepository(AppDbContext db) : Repository<User>(db), IUserRepository
{
    public void Update(User user)
    {
        db.Update(user);
    }
}
