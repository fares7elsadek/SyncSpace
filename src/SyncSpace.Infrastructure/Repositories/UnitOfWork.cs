using SyncSpace.Domain.Entities;
using SyncSpace.Domain.Repositories;
using SyncSpace.Infrastructure.Data;

namespace SyncSpace.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public IMessageRepository Messages { get; private set; }

    public IRoomsRepository Room { get; private set; }

    public IRoomParticipantsRepository RoomParticipants { get; private set; }

    public IUserRepository User { get; private set; }

    public IVideoSyncEventsRepository VideoSyncEvents { get; private set; }
    private readonly AppDbContext _db;
    public UnitOfWork(AppDbContext db)
    {
        _db = db;
        Messages = new MessagesRepository(db);
        Room = new RoomsRepository(db);
        RoomParticipants = new RoomParticipantsRepository(db);
        VideoSyncEvents = new VideoSyncEventsRepository(db);
        User = new UserRepository(db);
    }
    public async Task SaveAsync()
    {
       await _db.SaveChangesAsync();
    }
}
