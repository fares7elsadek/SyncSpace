using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SyncSpace.Domain.Entities;

namespace SyncSpace.Infrastructure.Data.config;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasDefaultValueSql("newid()");

        builder.Property(x => x.Avatar)
            .HasColumnType("text");

        builder.OwnsMany(x => x.RefreshTokens);
    }
}
