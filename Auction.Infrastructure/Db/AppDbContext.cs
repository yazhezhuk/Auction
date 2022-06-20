using Auction.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Db;

public class AppDbContext : IdentityDbContext<AuctionUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Lot> Lots { get; set; }
    public DbSet<LotBet> LotBets { get; set; }
}