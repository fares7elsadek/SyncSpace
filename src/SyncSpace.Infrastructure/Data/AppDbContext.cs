using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SyncSpace.Domain.Entities;
using SyncSpace.Infrastructure.Data.config;

namespace SyncSpace.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options):IdentityDbContext<User>(options)
{
    public DbSet<Rooms> Rooms { get; set; }
    public DbSet<Messages> Messages { get; set; }
    public DbSet<VideoSyncEvents> VideoSyncEvents { get; set; }
    public DbSet<RoomParticipants> RoomParticipants { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(RoomParticipantsConfigurations).Assembly);
        base.OnModelCreating(builder);
    }
}
