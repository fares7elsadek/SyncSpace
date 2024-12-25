namespace SyncSpace.Domain.Repositories;

public interface IUnitOfWork
{
    IMessageRepository Messages { get; }
    IRoomsRepository Room { get; }
    IRoomParticipantsRepository RoomParticipants { get; }
    IUserRepository User { get; }
    IVideoSyncEventsRepository VideoSyncEvents { get; }

    Task SaveAsync();
}
