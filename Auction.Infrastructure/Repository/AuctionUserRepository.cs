using Auction.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Repository;

public class AuctionUserRepository : BaseEfRepository<AuctionUser,string>
{
    public AuctionUserRepository(DbContext context) : base(context) { }
    
    
    
}