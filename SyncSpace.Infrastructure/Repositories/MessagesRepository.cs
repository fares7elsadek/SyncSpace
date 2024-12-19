using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Repositories;
using SyncSpace.Infrastructure.Data;

namespace SyncSpace.Infrastructure.Repositories;

public class MessagesRepository(AppDbContext db) : Repository<Messages>(db), IMessageRepository
{
    public void Update(Messages message)
    {
        db.Update(message);
    }
}
