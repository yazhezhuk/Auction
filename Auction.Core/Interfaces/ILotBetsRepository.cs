using Auction.Core.Entities;

namespace Auction.Core.Interfaces;

public interface ILotBetsRepository : IRepository<LotBet,int>
{
    Task<ICollection<LotBet>> GetAllWithDetailsAsync();
}