namespace Auction.Core.Entities;

public class Item : BaseIntEntity
{
    public string Description { get; set; }
    public byte[] Photo { get; set; }

    public int AuctionUserId { get; set; }
    public AuctionUser Owner { get; set; }
}