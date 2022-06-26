using Microsoft.AspNetCore.Identity;

namespace Auction.Infrastructure.Db.Initializer;

public static class DefaultDbInitializer
{
    public static void Initialize(AppDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            if (!context.Roles.Any(r => r.Name == "User"))
                context.Roles.Add(new IdentityRole("User")
                {
                    NormalizedName = "USER"
                });
            
            if (!context.Roles.Any(r => r.Name == "Admin"))
                context.Roles.Add(new IdentityRole("Admin")
                {
                    NormalizedName = "ADMIN"
                });
            
            context.SaveChanges();
        }
}