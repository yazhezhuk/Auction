using System.Data.SqlTypes;

namespace Auction.Core.Entities;

public enum LotStatus
{
    AwaitingForAccept,
    Scheduled,
    Started,
    Ended
}

public class Lot : BaseIntEntity
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public LotStatus LotStatus { get; set; }
    
    public int ItemId { get; set; }
    public Item? Item { get; set; }
    
    public ICollection<LotBet>? Bets { get; set; }
    public LotBet? MaxBet => Bets?.MaxBy(bet => bet.BetAmount);
    public decimal MinBet { get; set; }
    public LotBet? LatestBet => Bets?.Last(); 
}