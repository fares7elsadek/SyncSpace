using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncSpace.Domain.Entities;

namespace SyncSpace.Infrastructure.Data.config;

public class RoomParticipantsConfigurations : IEntityTypeConfiguration<RoomParticipants>
{
    public void Configure(EntityTypeBuilder<RoomParticipants> builder)
    {
        builder.HasKey(x => x.ParticipantId);
        builder.Property(x => x.ParticipantId)
            .HasDefaultValueSql("newid()");

        builder.Property(x => x.JoinedAt)
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne(x => x.User)
            .WithMany(x => x.ParticipatedRooms)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
