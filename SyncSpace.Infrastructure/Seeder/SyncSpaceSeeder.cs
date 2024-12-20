

using Microsoft.AspNetCore.Identity;
using SyncSpace.Domain.Constants;
using SyncSpace.Infrastructure.Data;

namespace SyncSpace.Infrastructure.Seeder;

public class SyncSpaceSeeder(AppDbContext db) : ISyncSpaceSeeder
{
    public async Task Seed()
    {
        if (await db.Database.CanConnectAsync())
        {
            if (!db.Roles.Any())
            {
                var roles = GetRoles();
                db.Roles.AddRange(roles);
                await db.SaveChangesAsync();
            }
        }
    }
    private IEnumerable<IdentityRole> GetRoles()
    {
        return [
            new(UserRoles.Admin)
            {
                NormalizedName= UserRoles.Admin.ToUpper()
            },
            new(UserRoles.User)
            {
                NormalizedName= UserRoles.User.ToUpper()
            }
        ];
    }
}
