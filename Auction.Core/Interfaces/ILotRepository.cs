using Auction.Core.Entities;

namespace Auction.Core.Interfaces;

public interface ILotRepository : IRepository<Lot,int>
{
    Task<ICollection<Lot>> GetAllWithDetailsAsync();
}