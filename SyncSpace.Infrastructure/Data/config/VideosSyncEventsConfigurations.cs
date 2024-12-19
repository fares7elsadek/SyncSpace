using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncSpace.Domain.Entities;

namespace SyncSpace.Infrastructure.Data.config;

public class VideosSyncEventsConfigurations : IEntityTypeConfiguration<VideoSyncEvents>
{
    public void Configure(EntityTypeBuilder<VideoSyncEvents> builder)
    {
        builder.HasKey(x => x.EventId);
        builder.Property(x => x.EventId)
            .HasDefaultValueSql("newid()");

        builder.Property(x => x.Timestamp)
            .HasColumnType("decimal")
            .HasPrecision(10, 2);
        builder.Property(x => x.TriggeredAt)
            .HasDefaultValueSql("GETDATE");

        builder.HasOne(x => x.User)
            .WithMany(x => x.VideoSyncEvents)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
