using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncSpace.Domain.Entities;

namespace SyncSpace.Infrastructure.Data.config;

public class RoomsConfigurations : IEntityTypeConfiguration<Rooms>
{
    public void Configure(EntityTypeBuilder<Rooms> builder)
    {
        builder.HasKey(x => x.RoomId);
        builder.Property(x => x.RoomId)
            .HasDefaultValueSql("newid()");

        builder.Property(x => x.VideoUrl)
            .HasColumnType("text");

        builder.Property(x => x.CurrentTime)
            .HasColumnType("decimal")
            .HasPrecision(10, 2);

        builder.Property(x => x.CreatedAt)
            .HasDefaultValueSql("GETDATE()");


        builder.HasOne(x => x.HostUser)
            .WithMany(x => x.HostingRooms)
            .HasForeignKey(x => x.HostUserId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(x => x.Messages)
            .WithOne(x => x.Room)
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Participants)
            .WithOne(x => x.Room)
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.VideoSyncEvents)
            .WithOne(x => x.Room)
            .HasForeignKey(x => x.RoomId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
