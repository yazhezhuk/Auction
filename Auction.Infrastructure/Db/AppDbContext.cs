using Auction.Core.Entities;
using Auction.Infrastructure.Db.Initializer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Db;

public class AppDbContext : IdentityDbContext<AuctionUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        DefaultDbInitializer.Initialize(this);
    }
    
    public DbSet<Lot> Lots { get; set; }
    public DbSet<LotBet> LotBets { get; set; }
}