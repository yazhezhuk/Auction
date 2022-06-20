using Auction.Core.Entities;
using Auction.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Auction.Infrastructure.Repository;

public class LotRepository : BaseEfRepository<Lot,int>, ILotRepository
{
    public LotRepository(DbContext context) : base(context)
    {
    }

    public async Task<ICollection<Lot>> GetAllWithDetailsAsync() => 
        await BaseSet
            .Include(x => x.Item)
            .Include(x => x.Bets)
            .ToListAsync();
    
}