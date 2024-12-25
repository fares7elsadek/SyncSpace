using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncSpace.Domain.Entities;

namespace SyncSpace.Infrastructure.Data.config;

public class MessagesConfigurations : IEntityTypeConfiguration<Messages>
{
    public void Configure(EntityTypeBuilder<Messages> builder)
    {
        builder.HasKey(x => x.MessageId);
        builder.Property(x => x.MessageId)
            .HasDefaultValueSql("newid()");

        builder.Property(x => x.Content)
            .HasColumnType("text");

        builder.Property(x => x.SentAt)
            .HasDefaultValueSql("GETDATE()");

        builder.HasOne(x => x.User)
            .WithMany(x => x.RoomMessages)
            .HasForeignKey(x =>x.UserId)
            .OnDelete(DeleteBehavior.NoAction);


    }
}
