using System.Data.SqlTypes;
using Ardalis.GuardClauses;

namespace Auction.Core.Entities;

public class LotBet : BaseIntEntity
{
    public LotBet(string userId, int lotId, decimal betAmount)
    {
        AuctionUserId = Guard.Against.NullOrEmpty(userId);
        LotId = Guard.Against.Negative(lotId);
        BetAmount = Guard.Against.NegativeOrZero(betAmount);
        BetTime = DateTime.Now;
    }
    
    public string AuctionUserId { get; set; }
    public AuctionUser? User { get; }
    
    public int LotId { get; set; }
    public Lot? Lot { get; }

    public decimal BetAmount { get; set; }
    public SqlDateTime BetTime { get; set; }
}